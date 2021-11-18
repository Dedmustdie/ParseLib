using System;
using System.Collections.Generic;
using System.Text;

namespace ParseLib.Core
{
    public interface IiKnigiParserSettings
    {
        // Адрес сайта
        string BaseUrl { get; set; }
        // префих страницы
        string Prefix { get; set; }
        // начало пагинации
        int StartPoint { get; set; }
        // конец пагинации
        int EndPoint { get; set; }
    }

}
