using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleOwl.Parse.Abstract
{
    public abstract class BaseParserSettings
    {
        private string url;
        public string BaseUrl { get; }
        public string Prefix { get; }
        public int StartPoint { get; set; }
        public int EndPoint { get; set; }

        public virtual string Url
        {
            get => url;
            set => url = value != BaseUrl ? BaseUrl + value : BaseUrl;
        }

        public BaseParserSettings(string baseUrl, string prefix)
        {
            BaseUrl = baseUrl;
            url = baseUrl;
            Prefix = prefix;
        }
    }
}
