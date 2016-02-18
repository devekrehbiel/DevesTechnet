using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Deves50.Controllers
{
    public class ServicesController : Controller
    {
        public ActionResult Accessories()
        {
            ViewBag.Message = "Jigs and Shop Helpers";

            return View();
        }
        public ActionResult EngineCart()
        {
            ViewBag.Message = "Engine Cart for your GM Stovebolt";

            return View();
        }
        public ActionResult HEISystem()
        {
            ViewBag.Message = "HEI Using Stock Distributor";

            return View();
        }
        public ActionResult HitchSystem()
        {
            ViewBag.Message = "Great Hitch Solution for Your AD";

            return View();
        }
        public ActionResult Index()
        {
            ViewBag.Message = "Farm-It-Out Home";

            return View();
        }
        public ActionResult RollerStand()
        {
            ViewBag.Message = "Shop Roller Stand";

            return View();
        }
        public ActionResult Rotisserie()
        {
            ViewBag.Message = "Frame Rotisserie";

            return View();
        }
        public ActionResult Shifter()
        {
            ViewBag.Message = "3 Speed Floor Shifter Info";

            return View();
        }

        public ActionResult StartKart()
        {
            ViewBag.Message = "Start Kart Info";

            return View();
        }
        public ActionResult SuperJig()
        {
            ViewBag.Message = "SuperJig Info";

            return View();
        }
        public ActionResult PCVUpgrade()
        {
            ViewBag.Message = "PCV Info";

            return View();
        }
        public ActionResult DipstickTube()
        {
            ViewBag.Message = "DipstickTube Info";

            return View();
        }
        public ActionResult OilFilterAdapter()
        {
            ViewBag.Message = "Oil Filter Adapter Info";

            return View();
        }
        public ActionResult HEIMount()
        {
            ViewBag.Message = "HEI Mount Plate Info";

            return View();
        }
        public ActionResult CarbAdapter()
        {
            ViewBag.Message = "Carb Adapter Info";

            return View();
        }
        public ActionResult HEIAdvert()
        {
            ViewBag.Message = "HEI Advertisement";

            return View();
        }
        public ActionResult BestHeaterControlSolution()
        {
            ViewBag.Message = "DC Motor Controller";

            return View();
        }
        public ActionResult RancoFirewallCover()
        {
            ViewBag.Message = "Heater Firewall Cover";

            return View();
        }
        public ActionResult CriticalInformationSystem()
        {
            ViewBag.Message = "Warning System";

            return View();
        }
        public ActionResult CISIntro()
        {
            ViewBag.Message = "CIS Intro";

            return View();
        }
    }
}