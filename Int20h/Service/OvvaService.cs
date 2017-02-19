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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TheArtOfDev.HtmlRenderer.WinForms;

namespace Int20h.Service
{
    public class OvvaService
    {
        public OvvaService()
        {

        }

        public IEnumerable<Models.Program> GetProgramForChannelByDay(string fileName, string lang, string date, string channel)
        {
            string url = $"https://api.ovva.tv/v2/{lang}/tvguide/{channel}/{date}";
            WebRequest webRequest = WebRequest.Create(url);
            HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();
            if (webResponse.StatusCode == HttpStatusCode.OK)
            {
                Stream stream = webResponse.GetResponseStream();
                if (stream != null)
                {
                    StreamReader streamReader = new StreamReader(stream);
                    string response = streamReader.ReadToEnd();
                    JToken programJson = JObject.Parse(response)["data"]["programs"];
                    var programs = JsonConvert.DeserializeObject<List<Models.Program>>(programJson.ToString());
                    streamReader.Close();
                    webResponse.Close();
                    return programs;
                }
            }
            return null;
        }

    }
}
