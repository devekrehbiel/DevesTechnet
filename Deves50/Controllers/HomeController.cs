using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Deves50.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult About()
        {
            ViewBag.Message = "About DevesTechNet.com.";

            return View();
        }
        public ActionResult AccessoriesPlans()
        {
            ViewBag.Message = "Cool Shop Accessories";

            return View();
        }
        public ActionResult ADWiringSystem()
        {
            ViewBag.Message = "Wiring Tips and Tricks";

            return View();
        }
        public ActionResult AirFilter()
        {
            ViewBag.Message = "Oil Bath or Modern Air?";

            return View();
        }
        public ActionResult Bedwood()
        {
            ViewBag.Message = "A Comprehensive Look";

            return View();
        }
        //*******************************************************************************Start Cab Related
        public ActionResult CabRelated()
        {
            ViewBag.Message = "Complete AD Cab Information";

            return View();
        }
        //*******************************************************************************Start 1st Json Set
        public JsonResult GetImagesFromOneCabDirectory(int imgId = 1)
        {
            var absolutePath = Server.MapPath("/Images/OtherSlideShows/OneCabJam/");
            return GetNextImageFilePath(absolutePath, imgId);
        }

        public JsonResult GetImagesFromOtherOneCabDirectory(int imgId = 1)
        {
            var absolutePath = Server.MapPath("/Images/OtherSlideShows/OtherOneCabJam/");
            return GetNextImageFilePath(absolutePath, imgId);
        }
        //*******************************************************************************End 1st Json Set
        //*******************************************************************************Start 2nd Json Set
        public JsonResult GetImagesFromTwoCabDirectory(int myimgId = 1)
        {
            var absolutePatha = Server.MapPath("/Images/OtherSlideShows/TwoCabJam/");
            return GetNextImageFilePath(absolutePatha, myimgId);
        }

        public JsonResult GetImagesFromOtherTwoCabDirectory(int myimgId = 1)
        {
            var absolutePatha = Server.MapPath("/Images/OtherSlideShows/OtherTwoCabJam/");
            return GetNextImageFilePath(absolutePatha, myimgId);
        }
        //*******************************************************************************End 2nd Json Set
        //*******************************************************************************End Cab Related
        public ActionResult CastingNumbers()
        {
            ViewBag.Message = "GM Casting Number Information";

            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Give Us Your Feedback!";

            return View();
        }
        public ActionResult Deve1950AResto()
        {
            ViewBag.Message = "Learning the Hard Way!";

            return View();
        }


        public ActionResult Deve1950BResto()
        {
            ViewBag.Message = "Deluxe Cab 1950 New Experiences!";

            return View();
        }
        public ActionResult DevesArchive()
        {
            ViewBag.Message = "Yesterday, Seems so Faraway!";

            return View();
        }
        public ActionResult DrumRemoval()
        {
            ViewBag.Message = "Small But Important Tip!";

            return View();
        }
        public ActionResult EngineCartPlans()
        {
            ViewBag.Message = "Making a Deluxe Engine Cart";

            return View();
        }
        public ActionResult Flywheel()
        {
            ViewBag.Message = "Flywheels Explained";

            return View();
        }
        public ActionResult TheVenerable261()
        {
            ViewBag.Message = "A Great Engine Choice for the AD";

            return View();
        }
        public ActionResult FuelFilter()
        {
            ViewBag.Message = "Strategy for Placement of the Fuel Filter";

            return View();
        }
        public ActionResult FullFlowOil()
        {
            ViewBag.Message = "A Great Way to Improve Oil Flow";

            return View();
        }
        public ActionResult Gallery()
        {
            ViewBag.Message = "Your Picture Gallery";

            return View();
        }
        public ActionResult GloveboxLock()
        {
            ViewBag.Message = "Removal and Replacement of the AD Glovebox Lock";

            return View();
        }
        public ActionResult HeadTorque()
        {
            ViewBag.Message = "Proper Stovebolt Torqueing Procedures";

            return View();
        }
        public ActionResult HeaterRestore()
        {
            ViewBag.Message = "Restoring Your Stock Heater";

            return View();
        }
        public ActionResult HEITuning()
        {
            ViewBag.Message = "Get the Best Tune for your HEI";

            return View();
        }
        public ActionResult History()
        {
            ViewBag.Message = "The Best Historical Information Ever!";

            return View();
        }
        public ActionResult HitchPlans()
        {
            ViewBag.Message = "The Last Hitch You Will Ever Need!";

            return View();
        }

        public ActionResult HowTo()
        {
            ViewBag.Message = "The Reason We are Here!";

            return View();
        }
        //*******************************************************************************Start How-To Slideshow Json
        private JsonResult GetNextImageFilePath(string path, int imgId)
        {
            if (path == "") throw new Exception("Have to have a path!");
            var allImageFiles = Directory.GetFiles(path).ToList();
            var myImageFiles = allImageFiles.Where(p => p.EndsWith("lg.jpg"));
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


        public string absolutePath { get; set; }

        public int imgId { get; set; }

        //*******************************************************************************End How-To Slideshow Json Pt 1
        //*******************************************************************************Start How-To Slideshow Json Pt 2
        private JsonResult GetNextImageFilePatha(string patha, int myimgId)
        {
            if (patha == "") throw new Exception("Have to have a path!");
            var allImageFilesa = Directory.GetFiles(patha).ToList();
            var myImageFilesa = allImageFilesa.Where(p => p.EndsWith("lg.jpg"));
            var myWebsitePatha = Server.MapPath("/");
            var myImageDictionarya = new Dictionary<int, string>(); // I'm using ReSharper so I have some autocompletes you don't
            var imageCountera = 1;
            foreach (var imageFilea in myImageFilesa)
            {
                var relativePathOfImagea = imageFilea.Replace(myWebsitePatha, "").Replace(@"\", "/"); // changes C:\blahblah\blahblah\myimage to "/Content/Images/MyImage.jpg" so it's friendly
                // THEORETICAL - I think it works but we have to try it!
                myImageDictionarya.Add(imageCountera, relativePathOfImagea);
                imageCountera++;
            }
            var totalImagea = imageCountera - 1;
            if (myimgId > totalImagea) myimgId = 1;
            if (myimgId < 1) myimgId = totalImagea;
            var currentImageStringa = myImageDictionarya[myimgId];

            return Json(new // BY DEFAULT... THIS DOES NOT LIKE HTTPGET
            {
                myimgId = myimgId,
                myimgPath = currentImageStringa
            }, JsonRequestBehavior.AllowGet); // PROBLEM SOLVED


        }
        public string absolutePatha { get; set; }

        public int myimgId { get; set; }
        //*******************************************************************************End How-To Slideshow Json Pt 2

        public ActionResult HubCapClips()
        {
            ViewBag.Message = "How to Install those Pesky Clips!";

            return View();
        }
        public ActionResult Index()
        {
            ViewBag.Message = "Start Your DTN Journey Here!";

            return View();
        }
        public JsonResult GetImagesFromFrontPageDirectory(int imgId = 1)
        {
            var absolutePath = Server.MapPath("/Images/OtherSlideShows/FrontPageAds/");
            return GetNextImageFilePath(absolutePath, imgId);
        }

        public JsonResult GetImagesFromOtherFrontPageDirectory(int imgId = 1)
        {
            var absolutePath = Server.MapPath("/Images/OtherSlideShows/FrontPageJam/");
            return GetNextImageFilePath(absolutePath, imgId);
        }
        public ActionResult LicensePlateResto()
        {
            ViewBag.Message = "Do It Yourself License Plate Restoration";

            return View();
        }
        public ActionResult LubeCharts()
        {
            ViewBag.Message = "Proper Chassis Lubrication";

            return View();
        }
        //*******************************************************************************Start MorePix
        public ActionResult MorePix()
        {
            ViewBag.Message = "Site Picture Gallery";

            return View();
        }

        public JsonResult GetImagesFromOneDirectory(int imgId = 1)
        {
            var absolutePath = Server.MapPath("/Images/Restorations/1950A/");
            return GetNextImageFilePath(absolutePath, imgId);
        }

        public JsonResult GetImagesFromOtherDirectory(int imgId = 1)
        {
            var absolutePath = Server.MapPath("/Images/Restorations/1950B/");
            return GetNextImageFilePath(absolutePath, imgId);
        }
        //*******************************************************************************End MorePix
        public ActionResult NutsAndBolts()
        {
            ViewBag.Message = "Fastener Information";

            return View();
        }
        public ActionResult PaintColors()
        {
            ViewBag.Message = "Paint Information for the AD";

            return View();
        }
        public ActionResult PCVInstall()
        {
            ViewBag.Message = "PCV Information for the AD";

            return View();
        }
        public ActionResult Project1954235()
        {
            ViewBag.Message = "A Complete Rebuild";

            return View();
        }
        public ActionResult Resources()
        {
            ViewBag.Message = "Resources Page";

            return View();
        }
        public ActionResult Restorations()
        {
            ViewBag.Message = "Restorations Page";

            return View();
        }
        //*******************************************************************************Start ROCHBCARB
        public ActionResult RochesterCarb()
        {
            ViewBag.Message = "All About The Rochester B";

            return View();
        }

        public JsonResult GetImagesFromCarbDirectory(int imgId = 1)
        {
            var absolutePath = Server.MapPath("/Images/OtherSlideShows/RochBCarbJam/");
            return GetNextImageFilePath(absolutePath, imgId);
        }

        public JsonResult GetImagesFromOtherCarbDirectory(int imgId = 1)
        {
            var absolutePath = Server.MapPath("/Images/OtherSlideShows/RochBCarbImg/");
            return GetNextImageFilePath(absolutePath, imgId);
        }
        //*******************************************************************************End ROCHBCARB
        public ActionResult RollerStandPlans()
        {
            ViewBag.Message = "Making a Roller Stand";

            return View();
        }
        public ActionResult RotisseriePlans()
        {
            ViewBag.Message = "Making a Frame Rotisserie";

            return View();
        }
        public ActionResult SandBlasting()
        {
            ViewBag.Message = "Sandblasting Made Easy?";

            return View();
        }
        public ActionResult SeatBelts()
        {
            ViewBag.Message = "A Seat Belt Primer";

            return View();
        }
        public ActionResult SeatDrawers()
        {
            ViewBag.Message = "A Seat Drawer How-To";

            return View();
        }
        public ActionResult SeatFrameRR()
        {
            ViewBag.Message = "Seat Frame R&R";

            return View();
        }
        public ActionResult ShifterAdjust()
        {
            ViewBag.Message = "3 Spd Floor Shifter Adjustment";

            return View();
        }
        public ActionResult SideEmblems()
        {
            ViewBag.Message = "Hood Emblem Primer";

            return View();
        }
        public ActionResult StakeRail()
        {
            ViewBag.Message = "Stake Rail Info";

            return View();
        }
        public ActionResult StartKartPlans()
        {
            ViewBag.Message = "Start Kart How-To";

            return View();
        }
        public ActionResult SteeringGearbox()
        {
            ViewBag.Message = "Gearbox Info";

            return View();
        }
        public ActionResult SteeringWheel()
        {
            ViewBag.Message = "Steering Wheel Repair";

            return View();
        }
        //*******************************************************************************Start Stock Ignition
        public ActionResult StockIgnition()
        {
            ViewBag.Message = "Stock Ignition Info";

            return View();
        }

        public JsonResult GetImagesFromIgnitionDirectory(int imgId = 1)
        {
            var absolutePath = Server.MapPath("/Images/OtherSlideShows/StockIgnitionJam/");
            return GetNextImageFilePath(absolutePath, imgId);
        }

        public JsonResult GetImagesFromOtherIgnitionDirectory(int imgId = 1)
        {
            var absolutePath = Server.MapPath("/Images/OtherSlideShows/StockIgnitionImg/");
            return GetNextImageFilePath(absolutePath, imgId);
        }
        //*******************************************************************************End Stock Ignition
        public ActionResult SuperJigPlans()
        {
            ViewBag.Message = "SuperJig How-To Info";

            return View();
        }
        public ActionResult SuperJigUse()
        {
            ViewBag.Message = "SuperJig Usage Info";

            return View();
        }
        public ActionResult TechNet()
        {
            ViewBag.Message = "Get the Most out of TechNet";

            return View();
        }
        public ActionResult TireCalc()
        {
            ViewBag.Message = "Tire Size Calc Info";

            return View();
        }
        public ActionResult TruckElectricals()
        {
            ViewBag.Message = "AD Electrical System Tech";

            return View();
        }
        //*******************************************************************************Start Tuneup Guide
        public ActionResult TuneUpGuide()
        {
            ViewBag.Message = "Stovebolt Tune-Up Tech";

            return View();
        }
        public JsonResult GetImagesFromTuneUpDirectory(int imgId = 1)
        {
            var absolutePath = Server.MapPath("/Images/OtherSlideShows/TuneUpGuideJam/");
            return GetNextImageFilePath(absolutePath, imgId);
        }

        public JsonResult GetImagesFromOtherTuneUpDirectory(int imgId = 1)
        {
            var absolutePath = Server.MapPath("/Images/OtherSlideShows/TuneUpGuideImg/");
            return GetNextImageFilePath(absolutePath, imgId);
        }
        //*******************************************************************************End Tuneup Guide
        public ActionResult TwelveVConversion()
        {
            ViewBag.Message = "Converting to 12 Volts";

            return View();
        }
        public ActionResult UnitySpotlight()
        {
            ViewBag.Message = "Unity Spotlight Installation";

            return View();
        }
        public ActionResult ValveAdjust()
        {
            ViewBag.Message = "Two Professionals, Two Procedures";

            return View();
        }
        public ActionResult ValveOiler()
        {
            ViewBag.Message = "Internal Oil Distribution";

            return View();
        }

        public ActionResult VehicleStorage()
        {
            ViewBag.Message = "Storing Your Vehicle";

            return View();
        }

        public ActionResult Project1959235()
        {
            ViewBag.Message = "Documenting a Total Rebuild";

            return View();
        }


        public ActionResult HEIgnition()
        {
            ViewBag.Message = "How To HEI";

            return View();
        }
        public ActionResult DipstickTubePlans()
        {
            ViewBag.Message = "How To DipstickTube";

            return View();
        }
        public ActionResult CarbAdapterPlans()
        {
            ViewBag.Message = "How To Carb Adapter";

            return View();
        }
        public ActionResult HEIMountPlans()
        {
            ViewBag.Message = "How To HEI Mount";

            return View();
        }
        public ActionResult HEIInstall()
        {
            ViewBag.Message = "How To Install";

            return View();
        }

        public ActionResult Native12VUpgrade()
        {
            ViewBag.Message = "How To Make Your Vehicle NATIVE 12v";

            return View();
        }

        public ActionResult WarningSystem()
        {
            ViewBag.Message = "Critical Systems Warning System";

            return View();
        }
    }
}







































        
        
        
        
        
        

        
        

       

        

        

       

        

        

        
        
       
