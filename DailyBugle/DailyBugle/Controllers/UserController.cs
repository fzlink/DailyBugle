using DailyBugle.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DailyBugle.Controllers
{
    
    public class UserController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        

        // GET: User
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
            return View();
        }

        [Authorize(Roles = "User, Editor, SuperAdmin")]
        public ActionResult News()
        {
            if(User.IsInRole("Editor"))
                return RedirectToRoute("EditorNewsPage");
            else if (User.IsInRole("Admin"))
                return RedirectToRoute("AdminNewsPage");

            DailyBugleDBEntities db = new DailyBugleDBEntities();
            List<News> newsList = db.News.ToList();
            List<NewsViewModel> newsListVM = newsList.Select(x => new NewsViewModel
            {
                NewsId = x.NewsId,
                Title = x.Title,
                Text = x.Text,
                AuthorName = x.AuthorName

            }).ToList();
            
            return View(newsListVM);
        }

        [Authorize(Roles = "User, Editor, SuperAdmin")]
        public ActionResult Details(int id)
        {
            NewsViewModel model = CreateModelWithId(id);
            return View(model);
        }

        private NewsViewModel CreateModelWithId(int id)
        {
            DailyBugleDBEntities db = new DailyBugleDBEntities();

            var news = db.News.Where(x => x.NewsId == id).FirstOrDefault();

            NewsViewModel model = new NewsViewModel()
            {
                NewsId = news.NewsId,
                Title = news.Title,
                AuthorName = news.AuthorName,
                Text = news.Text,
                Thumbnail = news.Thumbnail
            };
            return model;
        }

        [Authorize(Roles = "User, Editor, SuperAdmin")]
        public ActionResult EditProfile()
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var user = userManager.FindById(User.Identity.GetUserId());

            EditProfileViewModel userVM = new EditProfileViewModel()
            {
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                UserName = user.UserName
            };


            return View(userVM);
        }

        [HttpPost]
        public ActionResult EditProfile(EditProfileViewModel model)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            ApplicationUser user = userManager.FindById(User.Identity.GetUserId());

            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.UserName = model.UserName;
            

            if (ModelState.IsValid)
            {
                context.Entry(user).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

    }
}