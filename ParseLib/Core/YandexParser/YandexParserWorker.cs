using AngleSharp.Html.Parser;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParseLib.Core.YandexParser
{
    public class YandexParserWorker
    {
        YandexParser parser;
        YandexParserSettings parserSettings;
        YandexHtmlLoader loader;
        public YandexParserWorker(YandexParser parser)
        {
            this.parser = parser;
        }
        public YandexParserWorker(YandexParser parser, YandexParserSettings parserSettings)
        {
            this.parser = parser;
            this.parserSettings = parserSettings;
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

        public YandexParserSettings Settings
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
            CategoriesWorker();
        }
        public void StartArticlesWorker()
        {
            ArticleWorker();
        }


        public event Action<object, List<List<string>>> OnNewDataArticles;
        public event Action<object, List<string>> OnNewDataCategory;
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
            OnNewDataCategory?.Invoke(this, result);
            OnCompleted?.Invoke(this);
        }

        private async void ArticleWorker()
        {
            // иначе получить страницу с соответствующим номером в виде строки 
            var source = await loader.GetHtml();
            // создать html парсер из библиотеки AngleSharp 
            var htmlParser = new HtmlParser();
            // получить документ (из библиотеки AngleSharp) для парсинга  
            var document = await htmlParser.ParseDocumentAsync(source);
            // парсим ссылку на страницу выбранной категории
            string hrefOfCategory = parser.ParseHrefOfCategory(document, Settings.Category);
            // получение кода страницы
            var sourceOfCategory = await loader.GetHtmlByHref(hrefOfCategory);
            // документ страницы категории
            var documentOfCategory = await htmlParser.ParseDocumentAsync(sourceOfCategory);
            // парсим все ссылки на статьи на странице категории
            var hrefOfArtclesList = parser.ParseHrefOfArticles(documentOfCategory);
            List<List<string>> result = new List<List<string>>();
            foreach (var href in hrefOfArtclesList)
            {
                var sourceOfArticle = await loader.GetHtmlByHref(href);
                var documentOfArticle = await htmlParser.ParseDocumentAsync(sourceOfArticle);
                result.Add(parser.ParseArticles(documentOfArticle));
            }
            // вызвать событие, связанное с получением данных 
            OnNewDataArticles?.Invoke(this, result);
            OnCompleted?.Invoke(this);
        }
    }
}
