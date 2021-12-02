using System;
using System.Collections.Generic;
using System.Text;

namespace ParseLib.Core.PictureParser
{
    public class PictureParserSettings
    {
        public PictureParserSettings() { }
        public PictureParserSettings(string Subject)
        {
            this.Subject = Subject;
        }
        public PictureParserSettings(string Subject, int Count)
        {
            this.Subject = Subject;
            this.Path = Path;
        }
        public PictureParserSettings(string Subject, int Count, string Path)
        {
            this.Subject = Subject;
            this.Path = Path;
            this.Count = Count;
        }
        public string BaseUrl { get; set; } = "https://images.rambler.ru/search?query=INPUT&utm_source=cerber%3A%3Aheader%3A%3Asearch&utm_content=search_img&utm_medium=menu&utm_campaign=self_promo";
        public string Subject { get; set; } = "";
        public string Path { get; set; } = "";
        public int Count { get; set; } = 5;
    }
}
