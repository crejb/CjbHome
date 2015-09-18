using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

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