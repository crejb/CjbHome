﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CjbHome.Models
{
    public class BlogPost
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string LinkText { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PostDate { get; set; }
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime PostTime { get; set; }
        [Required]
        [AllowHtml]
        public string Content { get; set; }

        [DataType(DataType.ImageUrl)]
        public string HeaderImageUrl { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }
}