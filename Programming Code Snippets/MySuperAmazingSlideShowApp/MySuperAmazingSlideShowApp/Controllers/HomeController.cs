using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySuperAmazingSlideShowApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public JsonResult GetImagesFromOneDirectory(int imgId = 1)
        {
            return GetNextImageFilePath(@"C:\Development\MySuperAmazingSlideShowApp\MySuperAmazingSlideShowApp\Content\Images", imgId);
        }

        public JsonResult GetImagesFromOtherDirectory(int imgId = 1)
        {
            return GetNextImageFilePath(@"C:\Development\MySuperAmazingSlideShowApp\MySuperAmazingSlideShowApp\Content\OtherImages", imgId);
        }

        private JsonResult GetNextImageFilePath(string path, int imgId)
        {
            if (path == "") throw new Exception("Have to have a path!");
            // let's see if I can remember how to do this...
            var myImageFiles = Directory.GetFiles(path);
            var myWebsitePath = Server.MapPath("/");
            var myImageDictionary = new Dictionary<int, string>(); // I'm using ReSharper so I have some autocompletes you don't
            var imageCounter = 1;
            foreach (var imageFile in myImageFiles)
            {
                var relativePathOfImage = imageFile.Replace(myWebsitePath, "").Replace(@"\", "/"); // changes C:\blahblah\blahblah\myimage to "/Content/Images/MyImage.jpg" so it's friendly
                // THEORETICAL - I think it works but we have to try it!
                myImageDictionary.Add(imageCounter, relativePathOfImage);
                imageCounter++;
            }
            var totalImage = imageCounter - 1;
            if (imgId > totalImage) imgId = 1;
            if (imgId < 1) imgId = totalImage;
            var currentImageString = myImageDictionary[imgId];

            return Json(new // BY DEFAULT... THIS DOES NOT LIKE HTTPGET
            {
                imgId = imgId,
                imgPath = currentImageString
            }, JsonRequestBehavior.AllowGet); // PROBLEM SOLVED
        }
    }
}