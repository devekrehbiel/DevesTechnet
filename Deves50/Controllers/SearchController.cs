using System;
using System.Web.Mvc;
using Deves50_Shared.Search;

namespace Deves50.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        public ActionResult Index(string term)
        {
            var results = LuceneSearch.Search(term);

            return View(results);
        }
    }
}