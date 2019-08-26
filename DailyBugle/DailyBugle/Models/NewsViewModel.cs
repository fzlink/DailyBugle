using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DailyBugle.Models
{
    public class NewsViewModel
    {
        public NewsViewModel()
        {
            Comments = new List<string>();
        }

        [Required]
        public int NewsId { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }

        //[Required]
        public byte[] Thumbnail { get; set; }

        [Required]
        public string AuthorName { get; set; }
        
        public List<string> Comments { get; set; }

        [Required]
        public string Category { get; set; }

    }
}