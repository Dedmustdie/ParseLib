using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ParseLib.Core.PictureParser
{
    class PicutreResponseStatusCheaker
    {
        public async Task<bool> CheckResponseStatusAsync(string href)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(href);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
