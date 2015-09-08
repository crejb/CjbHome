using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CjbHome.Models;

namespace CjbHome.ViewModels.Blog
{
    public class ViewPostViewModel
    {
        public BlogPost Post { get; set; }
        public BlogPost NextPost { get; set; }
        public BlogPost PreviousPost { get; set; }

        public bool PostRequiresSyntaxHighlight { get; set; }
    }
}