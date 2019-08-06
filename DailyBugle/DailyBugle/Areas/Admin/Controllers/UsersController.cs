using DailyBugle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DailyBugle.Areas.Admin.Controllers
{
    public class UsersController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        

        // GET: Admin/Users
        public ActionResult Index()
        {   
            return View(context.Users.ToList());
        }

        public ActionResult Details(string id)
        {
            return View(context.Users.Find(id));
        }

        public ActionResult Delete(string id)
        {
            return View(context.Users.Find(id));
        }

        [HttpPost,ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {

            var user = context.Users.Where(x => x.Id == id).FirstOrDefault();
            context.Users.Remove(user);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}