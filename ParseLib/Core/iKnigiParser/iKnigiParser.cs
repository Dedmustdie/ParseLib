using AngleSharp.Html.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParseLib.Core.HabrParser
{
    public class iKnigiParser : IiKnigiParser<List<string>>
    {
        public List<string> Parse(IHtmlDocument document)
        {
            var items = document.QuerySelectorAll("a, div").Where(item =>
            (item.ParentElement.ParentElement.ClassName != null &&
            item.ParentElement.ParentElement.ClassName.Contains("comment") &&
            item.ClassName == null) ||
            (item.ParentElement.ClassName != null &&
            item.ParentElement.ClassName.Contains("comment-text")));
            var list = new List<string>();
            foreach (var item in items)
            {
                list.Add(item.TextContent);
            }
            return list;
        }
    }
}
