using CjbHome.Models;
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
        public static string GetExportedData(IEnumerable<BlogPost> posts)
        {
            XmlSerializer serialiser = new XmlSerializer(typeof(BlogPost[]));

            using (StringWriter sw = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sw))
                {
                    serialiser.Serialize(writer, posts.ToArray());
                    var xml = sw.ToString(); // Your XML
                    return xml;
                }
            }
        }
    }
}