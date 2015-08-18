using CjbHome.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CjbHome.ViewModels.Home
{
    public class IndexViewModel
    {
        public IEnumerable<BlogPost> RecentPosts { get; set; }
    }
}