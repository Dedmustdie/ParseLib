using AngleSharp.Html.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParseLib.Core.PictureParser
{
    public class PictureParser
    {
        public List<string> ParsePicturesHrefList(IHtmlDocument document, int count)
        {
            var items = document.QuerySelectorAll("a").Where(item =>
            item.ClassName == "Images__content__link--2YS4v");
            var PicturesHrefList = new List<string>();
            if (items.Count() > count)
            {
                for (int i = 0; i < count; i++)
                {
                    PicturesHrefList.Add(items.ToList()[i].GetAttribute("href"));
                }
            }
            else
            {
                foreach (var item in items)
                {
                    PicturesHrefList.Add(item.GetAttribute("href"));
                }
            }
            return PicturesHrefList;
        }
        public string ParsePictureURI(IHtmlDocument document)
        {
            var item = document.QuerySelectorAll("img").Where(item =>
            item.ClassName == "ImageBig__image--zFmRO");
            string uri = item.FirstOrDefault().GetAttribute("src");
            return uri;
        }
    }
}
