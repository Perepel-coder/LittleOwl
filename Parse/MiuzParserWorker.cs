using AngleSharp.Browser;
using LittleOwl.Models;
using LittleOwl.Parse.Abstract;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleOwl.Parse
{
    public class MiuzParserWorker : BaseParserWorker<Task<Product[]>>
    {
        public IEnumerable<Product>? Products { get; private set; }
        public MiuzParserWorker(BaseParser<Task<Product[]>> parser, BaseParserSettings settings) : base(parser, settings)
        {
        }


        public override void Run()
        {
            Task[] tasks = new Task[Settings.EndPoint - Settings.StartPoint + 1];
            Products = new List<Product>();

            for (int i = Settings.StartPoint, j = 0; i <= Settings.EndPoint; i++, j++)
            {
                var current_page = Settings.Prefix + i;
                var href = Settings.Url! + current_page;

                tasks[j] = Task.Run(async () =>
                {
                    Console.WriteLine($"Процесс: обработка страницы {current_page} -> начало");

                    var document = await Task.Run(() => HtmlLoader.GetHtmlDocument(href));
                    var items = await Task.Run(() => Parser.Parse(document));
                    Products = Products.Concat(items);

                    Console.WriteLine($"Процесс: обработка страницы {current_page} -> конец");
                });
            }
            Task.WaitAll(tasks);
        }
    }
}
