using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ParseLib.Core.PictureParser
{
    public class PictureDownloader
    {
        public void PictureDownloadAsync(string path, List<string> hrefList)
        {
            WebClient client = new WebClient();
            client.Headers.Add("user-agent",
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/59.0.3071.115 Safari/537.36");
            int i = 1;
            foreach (var href in hrefList)
            {
                Uri uri = new Uri(href);
                if (path == "")
                    client.DownloadFile(uri, "picture" + i + ".JPEG");
                else
                    client.DownloadFile(uri, path + "/picture" + i + ".JPEG");
                i++;
            }
        }
    }
}
