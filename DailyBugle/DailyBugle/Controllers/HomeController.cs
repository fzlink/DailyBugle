using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DailyBugle.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (User.IsInRole("SuperAdmin"))
            {
                return RedirectToRoute("AdminHomePage");
            }
            else if (User.IsInRole("Editor"))
            {
                return RedirectToRoute("EditorHomePage");
            }
            else if (User.IsInRole("User"))
            {
                return RedirectToRoute("UserHomePage");
            }
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
    }
}