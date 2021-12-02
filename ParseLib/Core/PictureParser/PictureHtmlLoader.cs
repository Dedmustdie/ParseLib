using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ParseLib.Core.PictureParser
{
    public class PictureHtmlLoader
    {
        readonly HttpClient client;
        readonly string url;
        public PictureHtmlLoader(PictureParserSettings settings)
        {
            client = new HttpClient();
            url = settings.BaseUrl;
        }
        public async Task<string> GetHtmlBySubjectPage(string subject)
        {
            // получение ответа на асинхронный запрос запрос get по ссылке 
            string s = url.Replace("INPUT", subject);
            var response = await client.GetAsync(url.Replace("INPUT", subject));
            string source = null;
            // проверка на null и статус ответа 
            if (response != null && response.StatusCode == HttpStatusCode.OK)
            {
                // получение исходного кода страницы в переменную source 
                source = await response.Content.ReadAsStringAsync();
            }
            return source;
        }
        public async Task<string> GetHtmlByFullSizeSubjectPage(string fullSizePictureUrl)
        {
            // получение ответа на асинхронный запрос запрос get по ссылке 
            var response = await client.GetAsync(fullSizePictureUrl);
            string source = null;
            // проверка на null и статус ответа 
            if (response != null && response.StatusCode == HttpStatusCode.OK)
            {
                // получение исходного кода страницы в переменную source 
                source = await response.Content.ReadAsStringAsync();
            }
            return source;
        }
    }
}
