using System;
using System.Collections.Generic;
using System.Text;
using AngleSharp.Html.Parser;

namespace ParseLib.Core.PictureParser
{
    public class PictureParserWorker
    {
        PictureParser parser = new PictureParser();
        PictureDownloader download = new PictureDownloader();
        PicutreResponseStatusCheaker cheaker = new PicutreResponseStatusCheaker();
        PictureParserSettings parserSettings;
        public PictureParserWorker()
        {
            parserSettings = new PictureParserSettings();
        }
        public PictureParserWorker(PictureParserSettings parserSettings)
        {
            this.parserSettings = parserSettings;
        }
        public PictureParser Parser
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

        public PictureParserSettings Settings
        {
            get
            {
                return parserSettings;
            }
            set
            {
                parserSettings = value;
            }
        }

        public void StartPictureWorker()
        {
            PictureWorker();
        }


        public event Action<object, string> UnsuccessfulDownload;
        public event Action<object, string> SuccessfulDownload;
        public event Action<object> OnCompleted;

        private async void PictureWorker()
        {
            PictureHtmlLoader loader = new PictureHtmlLoader(parserSettings);
            // иначе получить страницу с соответствующим номером в виде строки 
            var source = await loader.GetHtmlBySubjectPage(parserSettings.Subject);
            // создать html парсер из библиотеки AngleSharp 
            var htmlParser = new HtmlParser();
            // получить документ (из библиотеки AngleSharp) для парсинга  
            var document = await htmlParser.ParseDocumentAsync(source);
            // запустить парсинг для получения заголовков  
            var hrefList = parser.ParsePicturesHrefList(document, parserSettings.Count);
            List<string> picturesURIList = new List<string>();
            int i = 0;
            // вызвать событие, связанное с получением данных 
            foreach (var href in hrefList)
            {
                var fullSizeSource = await loader.GetHtmlByFullSizeSubjectPage(href);
                var fullSizeDocument = await htmlParser.ParseDocumentAsync(fullSizeSource);
                string PictureURI = parser.ParsePictureURI(fullSizeDocument);
                try
                {
                    if (await cheaker.CheckResponseStatusAsync(PictureURI))
                    {
                        picturesURIList.Add(parser.ParsePictureURI(fullSizeDocument));
                        SuccessfulDownload?.Invoke(this, PictureURI);
                    }
                    else UnsuccessfulDownload?.Invoke(this, PictureURI);
                }
                catch
                {
                    UnsuccessfulDownload?.Invoke(this, PictureURI);
                }

            }
            download.PictureDownloadAsync(parserSettings.Path, picturesURIList);           
            OnCompleted?.Invoke(this);
        }

    }
}

