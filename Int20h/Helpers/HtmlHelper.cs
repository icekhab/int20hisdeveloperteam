using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HandlebarsDotNet;
using TheArtOfDev.HtmlRenderer.Adapters;
using TheArtOfDev.HtmlRenderer.Core;
using TheArtOfDev.HtmlRenderer.WinForms;

namespace Int20h.Helpers
{
    public static class HtmlHelper
    {
        public static void SaveHtmlToImage(string html, string imageName)
        {
            Image image = HtmlRender.RenderToImage(html);
            //Image resizedImage = image.GetThumbnailImage(600, 10000, null, IntPtr.Zero);
            //resizedImage.Save(imageName, ImageFormat.Png);

            image.Save(imageName, ImageFormat.Png);
        }

        public static string RenderHtml(IEnumerable<Models.CostilDto> programs, string date)
        {
            string path = "Template.hbs";

            if (File.Exists(path))
            {
                var templateFile = File.ReadAllText(path);
                var template = Handlebars.Compile(templateFile);
                var data = new { Costil = programs, Date = date};
                return template(data);
            }
            return string.Empty;
        }
    }
}
