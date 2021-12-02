using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParseLib.Core.YandexParser
{
    public class YandexParser
    {
        public List<string> ParseCategory(IHtmlDocument document)
        {
            var items = document.QuerySelectorAll("div").Where(item => 
            item.ClassName == "zNFvW");
            var CategoryList = new List<string>();
            foreach (var item in items)
            {
                CategoryList.Add(item.TextContent);
            }
            return CategoryList;
        }
        public string ParseHrefOfCategory(IHtmlDocument document, string category)
        {
            var items = document.QuerySelectorAll("a").Where(item => item.ClassName == "_2qvOO _19m_j _3Vtwr");
            string href = "";
            foreach (var item in items)
            {
                var items2 = item.QuerySelector("div._2tlms");
                var items3 = items2.QuerySelector("div._3sQiG");
                var items4 = items3.QuerySelector("div.zNFvW");
     
                char[] charItem4 = items4.TextContent.ToCharArray();
                char[] charCatregory = category.ToCharArray();
                bool flag = true;

                if (charCatregory.Length == charItem4.Length)
                {
                    for (int i = 0; i < charCatregory.Length; i++)
                    {
                        if (char.IsLetterOrDigit(charItem4[i]) && (charItem4[i] != charCatregory[i]))
                        {
                            flag = false;
                        }
                    }

                    if (flag == true)
                    {
                        href = item.GetAttribute("href");
                    }
                }
            }
            return href.Replace("/journal", "");
        }
        public List<string> ParseHrefOfArticles(IHtmlDocument document)
        {
            var items = document.QuerySelectorAll("a").Where(item => item.ClassName == "_2qvOO _19m_j _2h-mG");       
            var hrefList = new List<string>();
            foreach (var item in items)
            {
                hrefList.Add(item.GetAttribute("href").Replace("/journal", ""));
            }
            return hrefList;
        }
        public List<string> ParseArticles(IHtmlDocument document)
        {
            var items = document.QuerySelectorAll("p");
            var ContentOfArticle = new List<string>();
            foreach (var item in items)
            {
                ContentOfArticle.Add(item.TextContent);
            }
            return ContentOfArticle;
        }
    }
}
