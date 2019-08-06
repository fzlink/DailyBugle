using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DailyBugle.Models
{
    public class NewsViewModel
    {
        public int NewsId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        //public byte[] Thumbnail { get; set; }
        public string AuthorName { get; set; }
    }
}