using DailyBugle.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DailyBugle.Areas.Editor.Controllers
{
    public class NewsController : Controller
    {
        DailyBugleDBEntities db = new DailyBugleDBEntities();

        // GET: Editor/News
        public ActionResult Index()
        {
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

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(NewsViewModel model)
        {

            News news = new News()
            {
                AuthorName = model.AuthorName,
                NewsId = model.NewsId,
                Text = model.Text,
                Title = model.Title
            };

            db.News.Add(news);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            NewsViewModel model = CreateModelWithId(id);
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            NewsViewModel model = CreateModelWithId(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(NewsViewModel model)
        {
            News news = db.News.Where(x => x.NewsId == model.NewsId).FirstOrDefault();

            news.Text = model.Text;
            news.Title = model.Title;
            news.AuthorName = model.AuthorName;

            if (ModelState.IsValid)
            {
                db.Entry(news).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        private NewsViewModel CreateModelWithId(int id)
        {

            var news = db.News.Where(x => x.NewsId == id).FirstOrDefault();

            NewsViewModel model = new NewsViewModel()
            {
                NewsId = news.NewsId,
                Title = news.Title,
                AuthorName = news.AuthorName,
                Text = news.Text
            };
            return model;
        }


    }
}