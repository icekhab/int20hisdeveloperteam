using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HandlebarsDotNet;
using Int20h.Helpers;
using Int20h.Models;
using Int20h.Service;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TheArtOfDev.HtmlRenderer.WinForms;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model.Attachments;
using VkNet.Model.RequestParams;

namespace Int20h
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTimeOffset date = DateTimeOffset.Now;
            string dateString = date.ToString("yyyy-MM-dd");
            string fileName = $"program{date.Ticks}.png";

            OvvaService ovva = new OvvaService();
            var programs = ovva.GetProgramForChannelByDay(fileName, ConstantInfo.Lang, dateString, ConstantInfo.Channel);

            //foreach (var program in programs)
            //{
            //    var olol = new DateTime(program.BeginTime);
            //    var olol1 = olol.ToString("t");
            //    program.Time = $"{new DateTime(program.BeginTime).ToString("t")} - {new DateTime(program.EndTime).ToString("t")}";
            //}

            var costils = CostilParse(programs);

            string html = HtmlHelper.RenderHtml(costils, dateString);
            HtmlHelper.SaveHtmlToImage(html, fileName);

            VkService vk = new VkService();
            vk.Authorize(ConstantInfo.AppId, ConstantInfo.Email, ConstantInfo.Password, Settings.All);
            var photos = vk.UploadImageInGroup((long)ConstantInfo.AlbumId, (long)ConstantInfo.GroupId, fileName);
            vk.WallPost("Hacaton Int20h", photos, (long)ConstantInfo.GroupId, true);
        }

        public static IEnumerable<CostilDto> CostilParse(IEnumerable<Models.Program> program)
        {
            List<CostilDto> costils = new List<CostilDto>();
            int column = (int)Math.Sqrt(program.Count());
            int j = 1;
            CostilDto costil = new CostilDto();
            costil.Programs = new List<Models.Program>();
            for (int i = 0; i < program.Count(); i++)
            {
                costil.Programs.Add(program.ElementAt(i));
                //j++;
                if (i == j * column - 1)
                {
                    j++;
                    costils.Add(costil);
                    costil = new CostilDto();
                    costil.Programs = new List<Models.Program>();
                }
            }
            return costils;
        }
    }
}
