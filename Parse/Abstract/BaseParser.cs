using AngleSharp.Html.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleOwl.Parse.Abstract
{
    public abstract class BaseParser<T> where T : class
    {
        public abstract T Parse(IHtmlDocument document);
    }
}
