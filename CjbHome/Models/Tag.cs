using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CjbHome.Models
{
    public class Tag
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }

        public virtual ICollection<BlogPost> BlogPosts { get; set; }
    }
}