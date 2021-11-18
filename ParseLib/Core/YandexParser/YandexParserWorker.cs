using AngleSharp.Html.Parser;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParseLib.Core.YandexParser
{
    public class YandexParserWorker
    {
        YandexParser parser;
        YandexSettings parserSettings;
        YandexHtmlLoader loader;
        bool isActive;
        public YandexParserWorker(YandexParser parser)
        {
            this.parser = parser;
        }
        public YandexParserWorker(YandexParser parser, YandexSettings parserSettings)
        {
            this.parser = parser;
            this.parserSettings = parserSettings;
        }
        public bool IsActive
        {
            get
            {
                return isActive;
            }
        }
        public YandexParser Parser
        {
            get
            {
                return parser;
            }
            set
            {
                parser = value;
            }
        }

        public YandexSettings Settings
        {
            get
            {
                return parserSettings;
            }
            set
            {
                parserSettings = value;
                loader = new YandexHtmlLoader(value);
            }
        }

        public void StartCategoriesWorker()
        {
            isActive = true;
            CategoriesWorker();
        }
        public void StartArticlesWorker()
        {
            isActive = true;
            ArticleWorker();
        }

        public void Abort()
        {
            isActive = false;
        }
        public event Action<object, List<string>> OnNewData;
        public event Action<object> OnCompleted;

        private async void CategoriesWorker()
        {
            // иначе получить страницу с соответствующим номером в виде строки 
            var source = await loader.GetHtml();
            // создать html парсер из библиотеки AngleSharp 
            var htmlParser = new HtmlParser();
            // получить документ (из библиотеки AngleSharp) для парсинга  
            var document = await htmlParser.ParseDocumentAsync(source);
            // запустить парсинг для получения заголовков  
            var result = parser.ParseCategory(document);
            // вызвать событие, связанное с получением данных 
            OnNewData?.Invoke(this, result);
            OnCompleted?.Invoke(this);
            isActive = false;
        }

        private async void ArticleWorker()
        {
            // иначе получить страницу с соответствующим номером в виде строки 
            var source = await loader.GetHtml();
            // создать html парсер из библиотеки AngleSharp 
            var htmlParser = new HtmlParser();
            // получить документ (из библиотеки AngleSharp) для парсинга  
            var document = await htmlParser.ParseDocumentAsync(source);
            // запустить парсинг для получения заголовков  
            string hrefOfCategory = parser.ParseHrefOfCategory(document, Settings.Category);
            var sourceArticles = await loader.GetHtmlByCategory(hrefOfCategory);
            var documentArticles = await htmlParser.ParseDocumentAsync(sourceArticles);
            var result = parser.ParseArticles(documentArticles);
            // вызвать событие, связанное с получением данных 
            OnNewData?.Invoke(this, result);
            OnCompleted?.Invoke(this);
            isActive = false;
        }
    }
}
