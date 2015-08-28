using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CjbHome.DataAccess;
using CjbHome.Models;
using CjbHome.ViewModels.Blog;

namespace CjbHome.Controllers
{
    public class BlogController : Controller
    {
        private readonly BlogPostDb _db = new BlogPostDb();

        // GET: Blog
        public ActionResult Index()
        {
            return View(_db.BlogPosts.ToList());
        }

        // GET: Blog/Title
        [Route("Blog/{id}")]
        public ActionResult ViewPost(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            BlogPost blogPost = _db.BlogPosts.FirstOrDefault(p => p.LinkText == id);
            if (blogPost == null)
            {
                return HttpNotFound();
            }

            // Get the previous post- the latest one before the current one
            var prevPost = _db.BlogPosts
                .Where(p => p.PostDate < blogPost.PostDate || (p.PostDate == blogPost.PostDate && p.PostTime < blogPost.PostTime))
                .OrderByDescending(p => p.PostDate)
                .ThenByDescending(p => p.PostTime)
                .FirstOrDefault();

            // Get the next post- the earliest one after the current one
            var nextPost = _db.BlogPosts
                .Where(p => p.PostDate > blogPost.PostDate || (p.PostDate == blogPost.PostDate && p.PostTime > blogPost.PostTime))
                .OrderBy(p => p.PostDate)
                .ThenBy(p => p.PostTime)
                .FirstOrDefault();

            var viewModel = new ViewPostViewModel
            {
                Post = blogPost,
                PreviousPost = prevPost,
                NextPost = nextPost
            };

            return View("ViewPost", viewModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
