using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace CjbHome.ViewHelpers
{
    public static class ViewHelper
    {
        public static MvcHtmlString RenderTinyMceEditorInitialisation(this HtmlHelper htmlHelper, string targetSelector)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<script src=\"//tinymce.cachefly.net/4.2/tinymce.min.js\"></script>");
            sb.AppendLine("<script>");
            sb.AppendLine("    tinymce.init(");
            sb.AppendLine("    {");
            sb.AppendFormat("        selector: \"{0}\",", targetSelector).AppendLine();
            sb.AppendLine("        plugins: \"link image code\",");
            sb.AppendLine("        valid_elements: \"*[*]\",");
            sb.AppendLine("        toolbar: [\"bold italic underline alignleft aligncenter alignright alignjustify styleselect fontsizeselect\",");
            sb.AppendLine("            \"bullist numlist outdent indent blockquote link image removeformat\"]");
            sb.AppendLine("    });");
            sb.AppendLine("</script>");

            return MvcHtmlString.Create(sb.ToString());
        }
    }
}