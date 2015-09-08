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
using System.IO;

namespace CjbHome.Controllers
{
    [Authorize]
    public class BlogAdminController : Controller
    {
        private BlogPostDb _db = new BlogPostDb();

        // GET: BlogAdmin
        public ActionResult Index()
        {
            return View(_db.GetSortedBlogPosts().ToList());
        }

        // GET: BlogAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BlogAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,LinkText,Title,PostDate,PostTime,Content,HeaderImageUrl")] BlogPost blogPost)
        {
            if (ModelState.IsValid)
            {
                _db.BlogPosts.Add(blogPost);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(blogPost);
        }

        // GET: BlogAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPost blogPost = _db.BlogPosts.Find(id);
            if (blogPost == null)
            {
                return HttpNotFound();
            }
            return View(blogPost);
        }

        // POST: BlogAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,LinkText,Title,PostDate,PostTime,Content,HeaderImageUrl")] BlogPost blogPost)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(blogPost).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(blogPost);
        }

        // GET: BlogAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPost blogPost = _db.BlogPosts.Find(id);
            if (blogPost == null)
            {
                return HttpNotFound();
            }
            return View(blogPost);
        }

        // POST: BlogAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BlogPost blogPost = _db.BlogPosts.Find(id);
            _db.BlogPosts.Remove(blogPost);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Export()
        {
            var xml = CjbHome.Services.ImportExportService.GetExportedData(_db.BlogPosts);
            var xmlBytes = System.Text.Encoding.ASCII.GetBytes(xml);
            var stream = new MemoryStream(xmlBytes);

            var filename = string.Format("CjbExport_{0}.xml", DateTime.Now.ToString("yyyyMMdd"));

            return new FileStreamResult(stream, "text/xml") { FileDownloadName = filename };
        }

        public ActionResult Import()
        {
            return View();
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
