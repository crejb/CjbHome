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

namespace CjbHome.Controllers
{
    public class BlogController : Controller
    {
        private BlogPostDb db = new BlogPostDb();

        // GET: Blog
        public ActionResult Index()
        {
            return View(db.BlogPosts.ToList());
        }

        // GET: Blog/Title
        [Route("Blog/{id}")]
        public ActionResult ViewPost(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPost blogPost = db.BlogPosts.FirstOrDefault(p => p.LinkText == id);
            if (blogPost == null)
            {
                return HttpNotFound();
            }
            return View("ViewPost", blogPost);
        }

        //// GET: Blog/5
        //[Route("Blog/{id}")]
        //public ActionResult ViewPost(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    BlogPost blogPost = db.BlogPosts.Find(id);
        //    if (blogPost == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View("ViewPost", blogPost);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
