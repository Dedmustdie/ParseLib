using AngleSharp.Html.Dom;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParseLib.Core
{
    public interface IiKnigiParser<T> where T : class
    {

        T Parse(IHtmlDocument document);

    }
}
