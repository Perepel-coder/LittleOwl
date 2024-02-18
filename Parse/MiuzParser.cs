using AngleSharp.Html.Dom;
using LittleOwl.Models;
using LittleOwl.Parse.Abstract;

namespace LittleOwl.Parse
{
    public class MiuzParser : BaseParser<Task<Product[]>>
    {
        public MiuzParser() { }

        private string _getCurrentCity(IHtmlDocument document)
        {
            return ((IHtmlSelectElement)document.QuerySelectorAll("select[name = CITY]")
                .First(i => ((IHtmlSelectElement)i).Value != null)).Value!;
        }

        private string? _getProductCities(IHtmlDocument document)
        {
            var cities = document.QuerySelectorAll("div.city-select > select > option")
                .Select(i => ((IHtmlOptionElement)i).Text);

            return string.Join(" , ", cities);
        }

        private string _getBreadCrumbs(IHtmlDocument document)
        {
            var bread_crumbs = document
                .QuerySelectorAll(".shops__breadcrumbs-items > .shops__breadcrumbs-item")
                .Select(i => new string(i.QuerySelector("span")?.TextContent.Trim()));

            return string.Join("/", bread_crumbs);
        }

        private string _getProductImages(IHtmlDocument document)
        {
            return string.Join(" , ", document.QuerySelectorAll("img").Where(i =>
                ((IHtmlImageElement)i).Title == "Цепь" ||
                ((IHtmlImageElement)i).Title == "Упаковка").Select(i =>
                ((IHtmlImageElement)i).Source));
        }

        public override async Task<Product[]> Parse(IHtmlDocument document)
        {
            var products_info = new List<Product>();

            var products = document.QuerySelectorAll("div").Where(item => item.ClassName == "product");

            var current_city_task = Task.Run(() => _getCurrentCity(document));

            foreach (var product in products)
            {
                var href = ((IHtmlAnchorElement)product.Children[0]).Href;

                document = await Task.Run(() => HtmlLoader.GetHtmlDocument(href));

                var product_cities_tasks = Task.Run(() => _getProductCities(document));
                var bread_crumbs_task = Task.Run(() => _getBreadCrumbs(document));
                var images_task = Task.Run(() => _getProductImages(document));
                var title_task = Task.Run(() => document.QuerySelector("div > h1")?.TextContent.TrimEnd());
                var price_task = Task.Run(() => document.QuerySelector(".detail__item-price-fact")?.TextContent.Trim());

                var current_city = await current_city_task;
                var product_cities = await product_cities_tasks;
                var bread_crumbs = await bread_crumbs_task;
                var images = await images_task;
                var title = await title_task;
                var price = await price_task;

                if(price!= null)
                {
                    products_info.Add(new Product(current_city, product_cities, bread_crumbs, images, title, price));
                }
            }

            return products_info.ToArray();
        }
    }
}
