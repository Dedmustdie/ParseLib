using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ParseLib.Core.HabrParser
{
    public class iKnigiHtmlLoader
    {
        readonly HttpClient client;
        readonly string url;  
        public iKnigiHtmlLoader(IiKnigiParserSettings settings)
        {
            client = new HttpClient();
            // url создается из адреса сайта и префикса страницы 
            url = $"{settings.BaseUrl}{settings.Prefix}/";
        }

        public async Task<string> GetSourceByPageId(int id)
        {
            // редактирование url для запроса 
            var currentUrl = url.Replace("{CurrentId}", id.ToString()); ;
            if (id == 1)
            {
                currentUrl = url.Replace("page/{CurrentId}/", "");
            }

            // получение ответа на асинхронный запрос запрос get по ссылке 
            var response = await client.GetAsync(currentUrl);
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
