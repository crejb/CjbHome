using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using System.Data.Entity.Migrations;
using System.Xml.Serialization;
using CjbHome.DataAccess;

namespace CjbHome.Services
{
    public class ImportExportService
    {
        public static string GetExportedData(IEnumerable<CjbHome.Models.BlogPost> posts)
        {
            var blogPosts = posts.ToList().Select(p => new BlogPost
            {
                Id = p.Id,
                LinkText = p.LinkText,
                Title = p.Title,
                PostDate = p.PostDate,
                PostTime = p.PostTime,
                Content = p.Content,
                HeaderImageUrl = p.HeaderImageUrl,
                Tags = p.Tags.Select(t => t.Title).ToArray()
            });

            XmlSerializer serialiser = new XmlSerializer(typeof(BlogPost[]));

            using (StringWriter sw = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sw))
                {
                    serialiser.Serialize(writer, blogPosts.ToArray());
                    var xml = sw.ToString(); // Your XML
                    return xml;
                }
            }
        }

        public static bool ImportedData(Stream stream, BlogPostDb db)
        {
            var deserialiser = new XmlSerializer(typeof(BlogPost[]));
            try
            {
                BlogPost[] posts;
                using (var streamReader = new StreamReader(stream))
                {
                    using (var xmlReader = XmlReader.Create(streamReader))
                    {
                        posts = (BlogPost[])deserialiser.Deserialize(xmlReader);
                    }
                }

                var newTags = new Dictionary<string, Models.Tag>();

                foreach (var post in posts)
                {
                    var dbPost = new CjbHome.Models.BlogPost
                    {
                        LinkText = post.LinkText,
                        Title = post.Title,
                        PostDate = post.PostDate,
                        PostTime = post.PostTime,
                        Content = post.Content,
                        HeaderImageUrl = post.HeaderImageUrl,
                        Tags = new List<CjbHome.Models.Tag>()
                    };

                    foreach (var tagString in post.Tags)
                    {
                        var blogTag = db.Tags.FirstOrDefault(t => t.Title == tagString);
                        if (blogTag == null)
                        {
                            if (!newTags.TryGetValue(tagString, out blogTag))
                            {
                                blogTag = new Models.Tag { Title = tagString };
                                db.Tags.Add(blogTag);
                                newTags.Add(tagString, blogTag);
                            }
                        }
                        dbPost.Tags.Add(blogTag);
                    }

                    var existingPost = db.BlogPosts.FirstOrDefault(p => p.LinkText == dbPost.LinkText);
                    if (existingPost != null)
                    {
                        db.BlogPosts.Remove(existingPost);
                    }

                    db.BlogPosts.Add(dbPost);
                }

                db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Helper class for serialisation
        /// </summary>
        public class BlogPost
        {
            public int Id { get; set; }
            public string LinkText { get; set; }
            public string Title { get; set; }
            public DateTime PostDate { get; set; }
            public DateTime PostTime { get; set; }
            public string Content { get; set; }
            public string HeaderImageUrl { get; set; }
            public string[] Tags { get; set; }
        }
    }
}