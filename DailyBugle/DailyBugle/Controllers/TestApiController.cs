using DailyBugle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DailyBugle.Controllers
{
    public class TestApiController : ApiController
    {
        public HttpResponseMessage Get()
        {

            using (DailyBugleDBEntities db = new DailyBugleDBEntities())
            {
                List<News> newsList = db.News.ToList();
                List<NewsViewModel> newsVMList = newsList.Select(x => new NewsViewModel()
                {
                    AuthorName = x.AuthorName,
                    Category = x.Category,
                    Title = x.Title


                }).ToList();
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, newsVMList);
                return response;
            }
            
        }
    }
}
