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
        public ActionResult Create([Bind(Include = "Id,LinkText,Title,PostDate,PostTime,Content,HeaderImageUrl")] BlogPost blogPost, string tags)
        {
            if (ModelState.IsValid)
            {
                SyncPostTags(blogPost, tags);

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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, string tags)
        {
            // Instead of using automatic model binding by having BlogPost as a param,
            // manually bind the fields so we can get the existing value for tags without 
            // automatically binding the new value
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var blogPost = _db.BlogPosts.Find(id);
            if (blogPost == null)
            {
                return HttpNotFound();
            }

            if (TryUpdateModel(blogPost, "", new []{"LinkText", "Title", "PostDate", "PostTime", "Content", "HeaderImageUrl"}))
            {
                SyncPostTags(blogPost, tags);

                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(blogPost);
        }

        private void SyncPostTags(BlogPost post, string tagsString)
        {
            var tagStrings = ParseTags(tagsString);

            if (post.Tags == null)
            {
                post.Tags = new List<Tag>();
            }

            var newPostTags = new List<Tag>();
            foreach (var tagString in tagStrings)
            {
                var tag = _db.Tags.FirstOrDefault(t => t.Title.Equals(tagString, StringComparison.CurrentCultureIgnoreCase));
                if (tag == null)
                {
                    tag = new Tag { Title = tagString };
                    _db.Tags.Add(tag);
                }
                newPostTags.Add(tag);


                if (!post.Tags.Any(t => t.Title == tag.Title))
                {
                    post.Tags.Add(tag);
                }
            }

            foreach (var tag in post.Tags.ToArray())
            {
                if (!newPostTags.Any(t => t.Title == tag.Title))
                {
                    post.Tags.Remove(tag);
                }
            }
        }

        private HashSet<string> ParseTags(string tagsString)
        {
            if (!string.IsNullOrWhiteSpace(tagsString))
            {
                return new HashSet<string>(
                    tagsString
                    .Split(',')
                    .Select(tag => tag.Trim()));
            }

            return new HashSet<string>();
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

        [HttpPost]
        public ActionResult Import(HttpPostedFileBase file)
        {
            if (file.ContentLength > 0)
            {
                var success = CjbHome.Services.ImportExportService.ImportedData(file.InputStream, _db);
                if(!success)
                {
                    ViewBag.Error = "The import was not successful. Please try again.";
                    return View();
                }
            }

            return RedirectToAction("Index");
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
