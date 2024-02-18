using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleOwl.Models
{
    public class Product
    {
        public string? CurrentCity { get; set; }
        public string? ProductCities { get; set; }
        public string? BreadCrumbs { get; set; }
        public string? Images { get; set; }
        public string? Title { get; set; }
        public string? Price { get; set; }

        public Product(
            string? current_city,
            string? products_cities,
            string? breadCrumbs,
            string? images,
            string? title,
            string? price)
        {
            CurrentCity = current_city;
            ProductCities = products_cities;
            BreadCrumbs = breadCrumbs;
            Images = images;
            Title = title;
            Price = price;
        }
    }
}
