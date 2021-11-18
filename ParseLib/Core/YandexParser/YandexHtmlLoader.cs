using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ParseLib.Core.YandexParser
{
    public class YandexHtmlLoader
    {
        readonly HttpClient client;
        readonly string url;
        public YandexHtmlLoader(YandexSettings settings)
        {
            client = new HttpClient();
            url = settings.BaseUrl;
        }

        public async Task<string> GetHtml()
        {
            // получение ответа на асинхронный запрос запрос get по ссылке 
            var response = await client.GetAsync(url);
            string source = null;
            // проверка на null и статус ответа 
            if (response != null && response.StatusCode == HttpStatusCode.OK)
            {
                // получение исходного кода страницы в переменную source 
                source = await response.Content.ReadAsStringAsync();
            }
            return source;
        }
        public async Task<string> GetHtmlByCategory(string href)
        {
            // получение ответа на асинхронный запрос запрос get по ссылке  
            var response = await client.GetAsync($"{url}+{href}");
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
