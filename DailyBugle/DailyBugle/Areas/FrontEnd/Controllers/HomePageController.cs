using DailyBugle.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.UI.HtmlControls;

namespace DailyBugle.Areas.FrontEnd.Controllers
{
    public class HomePageController : Controller
    {
        DailyBugleDBEntities db = new DailyBugleDBEntities();
        private int startIndex = 0;
        private int pageSize = 12;
        // GET: FrontEnd/HomePage

        
        public ActionResult Index(int page=0)
        {
            startIndex = page * pageSize;
            List<News> newsList = db.News.ToList();
            List<NewsViewModel> newsVMList = new List<NewsViewModel>();
            foreach (News news in newsList.Skip(startIndex).Take(pageSize))
            {
                NewsViewModel newsVM = new NewsViewModel()
                {
                NewsId = news.NewsId,
                AuthorName = news.AuthorName,
                Text = news.Text,
                Thumbnail = news.Thumbnail,
                Title = news.Title
                };

            newsVMList.Add(newsVM);
                
            }
            ViewBag.PageCount = (newsList.Count()) / pageSize;
            ViewBag.PageMod = ((newsList.Count() % pageSize) == 0);
            ViewBag.Cat = false;
            return View("Index", newsVMList);
        }

        [ActionName("CatIndex")]
        public ActionResult Index(string CategoryName,int page=0)
        {
            startIndex = page * pageSize;
            List<News> newsList = db.News.Where(m => m.Category == CategoryName).ToList();
            List<NewsViewModel> newsListVM = newsList.Skip(startIndex).Take(pageSize).Select(x => new NewsViewModel
            {
                NewsId = x.NewsId,
                AuthorName = x.AuthorName,
                Category = x.Category,
                Text = x.Text,
                Thumbnail = x.Thumbnail,
                Title = x.Title
            }).ToList();

            ViewBag.PageCount = (newsList.Count()) / pageSize;
            ViewBag.PageMod = ((newsList.Count() % pageSize) == 0);
            ViewBag.Cat = true;
            return View("Index",newsListVM);
        }

        public ActionResult NewsContent(int id)
        {
            News news = db.News.Where(m=> m.NewsId == id).FirstOrDefault();
            NewsViewModel model = new NewsViewModel()
            {
                AuthorName = news.AuthorName,
                NewsId = news.NewsId,
                Text = news.Text,
                Thumbnail = news.Thumbnail,
                Title = news.Title
            };
            ViewBag.Comments = UpdateCommentDB(news.NewsId);
            return View(model);
        }

        public ActionResult SearchNews(string searchText)
        {
            List<News> newsList = new List<News>();
            newsList = db.News.Where(x => x.Title.StartsWith(searchText) ).ToList();
            List<NewsViewModel> newsVMList = new List<NewsViewModel>();
            foreach(var news in newsList)
            {
                NewsViewModel model = new NewsViewModel()
                {
                    AuthorName = news.AuthorName,
                    Category = news.Category,
                    NewsId = news.NewsId,
                    Text = news.Text,
                    Thumbnail = news.Thumbnail,
                    Title = news.Title
                };
                newsVMList.Add(model);
            }
            return View("Index",newsVMList);
        }

        [HttpPost]
        public JsonResult SaveComment(string comment,int id, string personName)
        {
            if (comment.Equals("") || personName.Equals(""))
                return Json("Hata",JsonRequestBehavior.AllowGet);
            News news = db.News.Where(m => m.NewsId == id).FirstOrDefault();
            Comment tmp = new Comment()
            {
                NewsID = id,
                Text = comment,
                PersonName = personName
            };
            db.Comments.Add(tmp);
            db.SaveChanges();
            ViewBag.Comments = UpdateCommentDB(id);
            var JsonModel = new
            {
                Text = comment,
                PersonName = personName
            };
            return Json(JsonModel,JsonRequestBehavior.AllowGet);
        }


        private List<Comment> UpdateCommentDB(int NewsId)
        {
            List<Comment> comments = new List<Comment>();
            foreach (var comment in db.Comments)
            {
                if (comment.NewsID == NewsId)
                    comments.Add(comment);
            }
            return comments;
        }

    }
}