using DailyBugle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DailyBugle.Controllers
{
    
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "User")]
        public ActionResult News()
        {
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
    }
}