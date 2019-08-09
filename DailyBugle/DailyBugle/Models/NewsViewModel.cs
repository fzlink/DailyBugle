using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DailyBugle.Models
{
    public class NewsViewModel
    {
        [Required]
        public int NewsId { get; set; }

        [Required]
        [StringLength(30)]
        public string Title { get; set; }

        [Required]
        public string Text { get; set; }

        //public byte[] Thumbnail { get; set; }

        [Required]
        public string AuthorName { get; set; }
        
    }
}