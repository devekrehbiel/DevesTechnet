using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Search.Highlight;
using Lucene.Net.Store;
using Version = Lucene.Net.Util.Version;

namespace Deves50_Shared.Search
{
    public static class LuceneSearch
    {
        private const int MaxContentLength = 250;

        public static string LuceneDir
        {
            get
            {
                return HttpContext.Current == null ? Path.Combine(AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\", @"Deves50\Lucene") : HttpContext.Current.Server.MapPath("/Lucene");
            }
        }
        private static FSDirectory _directoryTemp;
        private static double _totalTime;
        private static int _totalResults;

        private static FSDirectory Directory
        {
            get
            {
                if (_directoryTemp == null) _directoryTemp = FSDirectory.Open(new DirectoryInfo(LuceneDir));
                if (IndexWriter.IsLocked(_directoryTemp)) IndexWriter.Unlock(_directoryTemp);
                var lockFilePath = Path.Combine(LuceneDir, "write.lock");
                var numberTries = 5;
                while (numberTries > 0 && File.Exists(lockFilePath))
                {
                    try
                    {
                        if (File.Exists(lockFilePath)) File.Delete(lockFilePath);
                    }
                    catch
                    {
                        --numberTries;
                        System.Threading.Thread.Sleep(1000);
                        Console.WriteLine("Number of tries: " + numberTries);
                    }
                }
                return _directoryTemp;
            }
        }

        private static void _addToLuceneIndex(SampleData sampleData, IndexWriter writer)
        {
            // remove older index entry
            //var searchQuery = new TermQuery(new Term("Id", sampleData.Id.ToString()));
            //writer.DeleteDocuments(searchQuery);

            // add new index entry
            var doc = new Document();

            // add lucene fields mapped to db fields
            doc.Add(new Field("Id", sampleData.Id.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
            doc.Add(new Field("Url", sampleData.Url, Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field("Title", sampleData.Title, Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.WITH_POSITIONS_OFFSETS));
            doc.Add(new Field("Contents", sampleData.Contents, Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.WITH_POSITIONS_OFFSETS));

            // add entry to index
            writer.AddDocument(doc);
        }

        public static void AddUpdateLuceneIndex(IEnumerable<SampleData> sampleDatas)
        {
            // init lucene
            var analyzer = new StandardAnalyzer(Version.LUCENE_30);
            using (var writer = new IndexWriter(Directory, analyzer, IndexWriter.MaxFieldLength.UNLIMITED))
            {
                // add data to lucene search index (replaces older entry if any)
                foreach (var sampleData in sampleDatas) _addToLuceneIndex(sampleData, writer);

                // close handles
                analyzer.Close();
                writer.Dispose();
            }
        }

        public static void AddUpdateLuceneIndex(SampleData sampleData)
        {
            AddUpdateLuceneIndex(new List<SampleData> { sampleData });
        }

        public static void ClearLuceneIndexRecord(int recordId)
        {
            // init lucene
            var analyzer = new StandardAnalyzer(Version.LUCENE_30);
            using (var writer = new IndexWriter(Directory, analyzer, IndexWriter.MaxFieldLength.UNLIMITED))
            {
                // remove older index entry
                var searchQuery = new TermQuery(new Term("Id", recordId.ToString()));
                writer.DeleteDocuments(searchQuery);

                // close handles
                analyzer.Close();
                writer.Dispose();
            }
        }

        public static bool ClearLuceneIndex()
        {
            try
            {
                var analyzer = new StandardAnalyzer(Version.LUCENE_30);
                using (var writer = new IndexWriter(Directory, analyzer, true, IndexWriter.MaxFieldLength.UNLIMITED))
                {
                    // remove older index entries
                    writer.DeleteAll();

                    // close handles
                    analyzer.Close();
                    writer.Dispose();
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public static void Optimize()
        {
            var analyzer = new StandardAnalyzer(Version.LUCENE_30);
            using (var writer = new IndexWriter(Directory, analyzer, IndexWriter.MaxFieldLength.UNLIMITED))
            {
                analyzer.Close();
                writer.Optimize();
                writer.Dispose();
            }
        }

        private static SampleData _mapLuceneDocumentToData(Document doc)
        {
            try
            {
                return new SampleData
                {
                    Id = Convert.ToInt32(doc.Get("Id")),
                    Url = doc.Get("Url"),
                    Title = doc.Get("Title"),
                    Contents = doc.Get("Contents"),
                    Total = _totalResults,
                    Time = _totalTime
                };
            }
            catch
            {
                return null;
            }
        }

        private static IEnumerable<SampleData> _mapLuceneToDataList(IEnumerable<Document> hits)
        {
            return hits.Select(_mapLuceneDocumentToData).ToList();
        }

        private static IEnumerable<SampleData> _mapLuceneToDataList(Query query, IEnumerable<ScoreDoc> hits, IndexSearcher searcher)
        {
            var sampleData = new List<SampleData>();
            foreach (var hit in hits)
            {
                var doc = searcher.Doc(hit.Doc);
                var scorer = new QueryScorer(query);
                var formatter = new SimpleHTMLFormatter("<span class=\"highlightedSearchTerm\">", "</span>");
                var highlighter = new Highlighter(formatter, scorer) {TextFragmenter = new SimpleFragmenter(MaxContentLength)}; // Max content length
                var stream = new StandardAnalyzer(Version.LUCENE_30).TokenStream("Contents", new StringReader(doc.Get("Contents")));

                sampleData.Add(new SampleData
                {
                    Id = Convert.ToInt32(doc.Get("Id")),
                    Url = doc.Get("Url"),
                    Title = doc.Get("Title"),
                    Contents = highlighter.GetBestFragments(stream, doc.Get("Contents"), 3, "..."),
                    Total = _totalResults,
                    Time = _totalTime
                });
                
            }
            return sampleData;
            //   return hits.Select(hit => _mapLuceneDocumentToData(query, searcher.Doc(hit.Doc))).ToList();
        }

        private static Query ParseQuery(string searchQuery, QueryParser parser)
        {
            Query query;
            try
            {
                query = parser.Parse(searchQuery.Trim());
            }
            catch (ParseException)
            {
                query = parser.Parse(QueryParser.Escape(searchQuery.Trim()));
            }
            return query;
        }

        private static IEnumerable<SampleData> _search(string searchQuery, string searchField = "")
        {
            // validation
            if (string.IsNullOrEmpty(searchQuery.Replace("*", "").Replace("?", ""))) return new List<SampleData>();

            // set up lucene searcher
            using (var searcher = new IndexSearcher(Directory, false))
            {
                const int hitsLimit = 1000;
                var analyzer = new StandardAnalyzer(Version.LUCENE_30);

                // search by single field
                if (!string.IsNullOrEmpty(searchField))
                {
                    var parser = new QueryParser(Version.LUCENE_30, searchField, analyzer);
                    var query = ParseQuery(searchQuery, parser);
                    //var hits = searcher.Search(query, hitsLimit).ScoreDocs;
                    Stopwatch timer = Stopwatch.StartNew();
                    var hitsTemp = searcher.Search(query, hitsLimit);
                    timer.Stop();
                    _totalTime = Math.Round(Convert.ToDouble(timer.ElapsedMilliseconds / 1000), 2);
                    _totalResults = hitsTemp.TotalHits;
                    var hits = hitsTemp.ScoreDocs;

                    var results = _mapLuceneToDataList(query, hits, searcher);
                    analyzer.Close();
                    searcher.Dispose();
                    return results;
                }
                // search by multiple fields (ordered by RELEVANCE)
                else
                {
                    var parser = new MultiFieldQueryParser(Version.LUCENE_30, new[] { "Id", "Url", "Title", "Contents" }, analyzer);
                    var query = ParseQuery(searchQuery, parser);
                    Stopwatch watch = Stopwatch.StartNew();
                    var hitsTemp = searcher.Search(query, null, hitsLimit, Sort.RELEVANCE);
                    watch.Stop();
                    _totalTime = (float)watch.ElapsedMilliseconds / 1000;
                    _totalTime = Math.Round(Convert.ToDouble(_totalTime), 2);
                    _totalResults = hitsTemp.TotalHits;
                    var hits = hitsTemp.ScoreDocs;
                    var results = _mapLuceneToDataList(query, hits, searcher);
                    analyzer.Close();
                    //searcher.Close();
                    searcher.Dispose();
                    return results;
                }
            }
        }

        public static IEnumerable<SampleData> Search(string input, string fieldName = "")
        {
            if (string.IsNullOrEmpty(input)) return new List<SampleData>();

            return _search(input, fieldName);
        }

        public static IEnumerable<SampleData> SearchDefault(string input, string fieldName = "")
        {
            return string.IsNullOrEmpty(input) ? new List<SampleData>() : _search(input, fieldName);
        }

        public static IEnumerable<SampleData> GetAllIndexRecords()
        {
            // validate search index
            if (!System.IO.Directory.EnumerateFiles(LuceneDir).Any()) return new List<SampleData>();

            // set up lucene searcher
            var searcher = new IndexSearcher(Directory, false);
            var reader = IndexReader.Open(Directory, false);
            var docs = new List<Document>();
            var term = reader.TermDocs();
            while (term.Next()) docs.Add(searcher.Doc(term.Doc));
            reader.Dispose();
            searcher.Dispose();
            return _mapLuceneToDataList(docs);
        }
    }
}