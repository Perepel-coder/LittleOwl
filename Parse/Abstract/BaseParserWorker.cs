using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleOwl.Parse.Abstract
{
    public abstract class BaseParserWorker<T> where T : class
    {
        public BaseParser<T> Parser { get; private set; }
        public BaseParserSettings Settings { get; private set; }

        public bool IsActive { get; private set; }

        public BaseParserWorker(BaseParser<T> parser, BaseParserSettings parserSettings)
        {
            Parser = parser;
            Settings = parserSettings;
        }

        public abstract void Run();
    }
}
