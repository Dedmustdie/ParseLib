using System;
using System.Collections.Generic;
using System.Text;

namespace ParseLib.Core.YandexParser
{
    public class YandexParserSettings
    {
        public YandexParserSettings(string Category)
        {
            this.Category = Category;
        }
        public string BaseUrl { get; set; } = "https://market.yandex.ru/journal";
        public string Category { get; set; }

    }
}
