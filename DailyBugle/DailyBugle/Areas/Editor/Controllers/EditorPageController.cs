using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DailyBugle.Areas.Editor.Controllers
{
    public class EditorPageController : Controller
    {
        // GET: Editor/EditorPage
        public ActionResult Index()
        {
            return View();
        }
    }
}