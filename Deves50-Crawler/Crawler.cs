using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web.UI.HtmlControls;
using Abot.Crawler;
using Deves50_Shared.Search;
using HtmlAgilityPack;

namespace Deves50_Crawler
{
    class Crawler
    {
        private static List<SampleData> _sampleData;

        static void Main()
        {
            _sampleData = new List<SampleData>();
            var crawler = new PoliteWebCrawler();
            crawler.PageCrawlStarting += crawler_ProcessPageCrawlStarting;
            crawler.PageCrawlDisallowed += crawler_PageCrawlDisallowed;
            crawler.PageCrawlCompleted += crawler_ProcessPageCrawlCompleted;
            crawler.PageLinksCrawlDisallowed += crawler_PageLinksCrawlDisallowed;
            crawler.Crawl(new Uri("http://devestechnet.com/"));
            LuceneSearch.ClearLuceneIndex();
            LuceneSearch.AddUpdateLuceneIndex(_sampleData);
            foreach (var s in _sampleData)
            {
                Console.WriteLine("{0} - {1}: {2}" + Environment.NewLine + "{3}", s.Id, s.Title, s.Url, s.Contents);
            }
            Console.WriteLine("Done. Press any key to exit.");
            Console.ReadKey();
        }

        static void crawler_ProcessPageCrawlStarting(object sender, PageCrawlStartingArgs e)
        {
            var pageToCrawl = e.PageToCrawl;
            Console.WriteLine("About to crawl link {0} which was found on page {1}", pageToCrawl.Uri.AbsoluteUri, pageToCrawl.ParentUri.AbsoluteUri);
        }

        private static void crawler_ProcessPageCrawlCompleted(object sender, PageCrawlCompletedArgs e)
        {
            var crawledPage = e.CrawledPage;

            if (crawledPage.WebException != null || crawledPage.HttpWebResponse.StatusCode != HttpStatusCode.OK)
            {
                Console.WriteLine("Crawl of page failed {0}", crawledPage.Uri.AbsoluteUri);
                return;
            }
            Console.WriteLine("Crawl of page succeeded {0}", crawledPage.Uri.AbsoluteUri);

            if (string.IsNullOrEmpty(crawledPage.Content.Text))
            {
                Console.WriteLine("Page had no content {0}", crawledPage.Uri.AbsoluteUri);
                return;
            }
            var sampleData = new SampleData
            {
                Title = GetPageTitle(crawledPage.Content.Text),
                Url = crawledPage.Uri.AbsoluteUri,
                Contents = StripHtmlTags(crawledPage.Content.Text)
            };

            _sampleData.Add(sampleData);
        }

        static void crawler_PageLinksCrawlDisallowed(object sender, PageLinksCrawlDisallowedArgs e)
        {
            var crawledPage = e.CrawledPage;
            Console.WriteLine("Did not crawl the links on page {0} due to {1}", crawledPage.Uri.AbsoluteUri, e.DisallowedReason);
        }

        static void crawler_PageCrawlDisallowed(object sender, PageCrawlDisallowedArgs e)
        {
            var pageToCrawl = e.PageToCrawl;
            Console.WriteLine("Did not crawl page {0} due to {1}", pageToCrawl.Uri.AbsoluteUri, e.DisallowedReason);
        }

        private static string GetPageTitle(string htmlContent)
        {
            if (string.IsNullOrEmpty(htmlContent)) return string.Empty;
            var doc = new HtmlDocument {OptionFixNestedTags = true};
            doc.LoadHtml(htmlContent);
            var titleNode = doc.DocumentNode.SelectSingleNode("//title");
            var title = titleNode == null ? doc.DocumentNode.InnerText : titleNode.InnerText;
            return title; //.Replace(" = Deve's TechNet", "");
        }

        private static string StripHtmlTags(string html)
        {
            if (String.IsNullOrEmpty(html))
                return string.Empty;
            var doc = new HtmlDocument { OptionFixNestedTags = true };
            doc.LoadHtml(html);
            doc.DocumentNode.Descendants()
                    .Where(n => n.Name == "script" || n.Name == "style" || n.Name == "#comment")
                    .ToList()
                    .ForEach(n => n.Remove());
            //Read the body content only.
            HtmlNode.ElementsFlags.Remove("form");
            HtmlNode.ElementsFlags.Remove("a");
            HtmlNode.ElementsFlags.Remove("img");

            var searchableBodyContent =
                doc.DocumentNode.Descendants("div")
                    .Where(
                        d =>
                            d.Attributes.Contains("class") &&
                            d.Attributes["class"].Value.Contains("searchableBodyContent"));
            var bodyNode = searchableBodyContent.First();
            //var bodyNode = doc.DocumentNode.SelectSingleNode("//body");
            var tags = bodyNode.SelectNodes("//form | //input | //select | //textarea");
            if (tags != null && tags.Count > 0)
            {
                for (var i = 0; i < tags.Count; i++)
                    tags.Remove(i);
            }
            var div = new HtmlGenericControl { InnerText = bodyNode.InnerText };
            var tempdivtext = div.InnerText;
            tempdivtext = tempdivtext.Replace("\r\n", string.Empty);
            tempdivtext = tempdivtext.Replace("\n", string.Empty);
            tempdivtext = tempdivtext.Replace("\t", string.Empty);
            tempdivtext = tempdivtext.Replace("&nbsp", string.Empty);
            tempdivtext = Regex.Replace(tempdivtext, @"\s+", " ");
            return tempdivtext.Trim();
        }
    }
}
