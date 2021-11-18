using AngleSharp.Html.Parser;
using ParseLib.Core.HabrParser;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParseLib.Core
{
    public class iKnigiParserWorker<T> where T : class
    {
        IiKnigiParser<T> parser;
        IiKnigiParserSettings parserSettings;
        iKnigiHtmlLoader loader;
        bool isActive;
        public iKnigiParserWorker(IiKnigiParser<T> parser) 
        {
            this.parser = parser;
        }
        public iKnigiParserWorker(IiKnigiParser<T> parser, IiKnigiParserSettings parserSettings) 
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
        public IiKnigiParser<T> Parser
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

        public IiKnigiParserSettings Settings
        {
            get
            {
                return parserSettings;
            }
            set
            {
                parserSettings = value;
                loader = new iKnigiHtmlLoader(value);
            }
        }

        public void Start()
        {
            isActive = true;
            Worker();
        }

        public void Abort()
        {
            isActive = false;
        }
        public event Action<object, T> OnNewData;
        public event Action<object> OnCompleted;
        
        private async void Worker()
        {
            // в цикле нужно пробежать все номера страниц от начальной до конечной 

            for (int i = parserSettings.StartPoint; i <= parserSettings.EndPoint; i++)
            {
                // если процесс парсинга остановлен 
                if (!isActive)
                {
                    // нужно вызвать завершающее событие  
                    OnCompleted?.Invoke(this);
                    // и завершить выполнение метода 
                    return;
                }
                // иначе получить страницу с соответствующим номером в виде строки 
                var source = await loader.GetSourceByPageId(i);
                // создать html парсер из библиотеки AngleSharp 
                var htmlParser = new HtmlParser();
                // получить документ (из библиотеки AngleSharp) для парсинга  
                var document = await htmlParser.ParseDocumentAsync(source);
                // запустить парсинг для получения заголовков  
                var result = parser.Parse(document);
                // вызвать событие, связанное с получением данных 
                OnNewData?.Invoke(this, result);
            }
            OnCompleted?.Invoke(this);

            isActive = false;
        }
        
    }
}
