using DailyBugle.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace DailyBugle.Areas.Admin.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class AdminPageController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        // GET: Admin/AdminPage

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateRole(FormCollection form)
        {
            string roleName = form["txtRoleName"];
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            if (!roleManager.RoleExists(roleName))
            {
                //create super admin
                var role = new IdentityRole(roleName);
                roleManager.Create(role);

            }
            return View("Index");
        }



        public ActionResult AssignRole()
        {
            ViewBag.Roles = context.Roles.Select(r => new SelectListItem { Value = r.Name, Text = r.Name }).ToList();
            return View();
        }

        [HttpPost]
        public ActionResult AssignRole(FormCollection form)
        {

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            string email = form["txtEmail"];
            string roleName = form["txtRoleName"];
            ApplicationUser user = context.Users.Where(x => x.Email.Equals(email, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

            userManager.AddToRole(user.Id, roleName);

            return View("Index");
        }




    }
}