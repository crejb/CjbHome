using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

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
        public DateTime PostTime { get; set; }
        [Required]
        public string Content { get; set; }
    }
}