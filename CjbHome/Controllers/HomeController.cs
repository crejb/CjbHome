using CjbHome.DataAccess;
using CjbHome.ViewModels.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CjbHome.Controllers
{
    public class HomeController : Controller
    {
        private readonly BlogPostDb _blogPostDb;

        public HomeController()
        {
            _blogPostDb = new BlogPostDb();
        }

        public ActionResult Index()
        {
            var recentPosts = _blogPostDb
                .GetSortedBlogPosts()
                .Take(3);
            var vm = new IndexViewModel { RecentPosts = recentPosts };
            return View(vm);
        }

        public ActionResult Projects()
        {
            return View();
        }


        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}