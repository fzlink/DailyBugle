using DailyBugle.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DailyBugle.Areas.Admin.Controllers
{
    public class NewsController : Controller
    {
        DailyBugleDBEntities db = new DailyBugleDBEntities();

        // GET: Admin/News

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
            ViewBag.Categories = GetCategoryList();
            return View();
        }

        [HttpPost]
        public ActionResult Create(NewsViewModel model, HttpPostedFileBase image)
        {



            ViewBag.Categories = GetCategoryList();
            News news = new News()
            {
                NewsId = model.NewsId,
                AuthorName = model.AuthorName,
                Text = model.Text,
                Title = model.Title,
                Category = model.Category
            };
            MemoryStream memory = new MemoryStream();
            image.InputStream.CopyTo(memory);
            news.Thumbnail = memory.ToArray();

            if (ModelState.IsValid)
            {
                db.News.Add(news);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public ActionResult Details(int id)
        {
            NewsViewModel model = CreateModelWithId(id);
            return View(model);
        }

        public ActionResult Edit(int id)
        {

            ViewBag.Categories = GetCategoryList();
            NewsViewModel model = CreateModelWithId(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(NewsViewModel model,HttpPostedFileBase image) {
            ViewBag.Categories = GetCategoryList();
            News news = db.News.Where(x => x.NewsId == model.NewsId).FirstOrDefault();

            news.Text = model.Text;
            news.Title = model.Title;
            news.AuthorName = model.AuthorName;
            news.Category = model.Category;

            MemoryStream memory = new MemoryStream();
            image.InputStream.CopyTo(memory);
            news.Thumbnail = memory.ToArray();

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
                Text = news.Text,
                Thumbnail = news.Thumbnail,
                Category = news.Category
            };
            return model;
        }

        public List<string> GetCategoryList()
        {
            List<string> listCategory = new List<string>();
            foreach (var cat in db.Categories)
            {
                listCategory.Add(cat.Name);
            }
            return listCategory;
        }

        public ActionResult Delete(int id)
        {
            NewsViewModel model = CreateModelWithId(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(NewsViewModel model)
        {
            News news = db.News.Where(x => x.NewsId == model.NewsId).FirstOrDefault();

            db.News.Remove(news);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}