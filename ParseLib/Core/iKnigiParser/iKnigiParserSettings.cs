using System;
using System.Collections.Generic;
using System.Text;

namespace ParseLib.Core.HabrParser
{
    public class iKnigiParserSettings : IiKnigiParserSettings
    {
        public int StartPoint { get; set; }
        public int EndPoint { get; set; }
        public int CurrentId { get; set; }
        public iKnigiParserSettings(int StartPoint, int EndPoint)
        {
            this.StartPoint = StartPoint;
            this.EndPoint = EndPoint;
        }
        public string BaseUrl { get; set; } = "https://iknigi.net/otzivi-na-knigi/";
        public string Prefix { get; set; } = "page/{CurrentId}" ;
    }
}
