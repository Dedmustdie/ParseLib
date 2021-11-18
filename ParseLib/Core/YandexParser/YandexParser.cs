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
            //var items = document.QuerySelectorAll("a._2qvOO _19m_j _3Vtwr");
            var items = document.QuerySelectorAll("a").Where(item => item.ClassName == "_2qvOO _19m_j _3Vtwr");
            string href = "";
            foreach (var item in items)
            {
                var items2 = item.QuerySelector("div._2tlms");
                var items3 = items2.QuerySelector("div._3sQiG");
                var items4 = items3.QuerySelector("div.zNFvW");

                char[] charItem4 = items4.TextContent.ToCharArray();
                char[] charCatregory = category.ToCharArray();
                bool flag = false;
                for (int i = 0; i < charCatregory.Length; i++)
                {
                    if (charItem4[i] != charCatregory[i])
                    {
                         flag = false;
                    }
                }

                if (flag == true)
                {
                    href = item.GetAttribute("href");
                }
                //if (items4.TextContent == category)
                //{
                //    href = item.GetAttribute("href");
                //}

                //if (charItem4 == charCatregory)
                //{
                //    href = item.GetAttribute("href");
                //}

                //if (items4.TextContent.Replace(" ", string.Empty).ToLower().Equals(category.Replace(" ", string.Empty).ToLower()))
                //{
                //    href = item.GetAttribute("href");
                //}

            }

            return href;
        }
        public List<string> ParseArticles(IHtmlDocument document)
        {
            //var items = document.QuerySelectorAll("a._2qvOO _19m_j _3Vtwr");
            var items = document.QuerySelectorAll("a").Where(item => item.ClassName == "_2qvOO _19m_j _3Vtwr");
            //foreach (var item in items)
            //{
            //    var items2 = item.QuerySelector("div._2tlms");
            //    var items3 = items2.QuerySelector("div._3sQiG");
            //    var items4 = items3.QuerySelector("div.zNFvW");
            //    if (items4.TextContent == category)
            //    {
            //        href = item.GetAttribute("href");
            //    }
            //}
            var ArtTitle = new List<string>();
            foreach (var item in items)
            {
                ArtTitle.Add(item.TextContent);
            }
            return ArtTitle;

        }
    }
}
