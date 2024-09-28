using Ecommerce_Project.Models.Images;
using Ecommerce_Project.Models.Prices;
using System;

namespace Ecommerce_Project.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ShortDescription { get; set; } = string.Empty;
        public string LongDescription { get; set; } = string.Empty;
        public double Price { get; set; }
        public double SoldPrice { get; set; }

        public string Brand { get; set; } = string.Empty;
        public string MadeIn { get; set; } = string.Empty;

        public long StockQuantity { get; set; }     // How many pieces
        public int MinimumOrder { get; set; }

        public DateTime PublicationDate { get; set; }

        // Foreign Keys
        public List<ProductPrice>? Prices { get; set; }
        public Subcategory Subcategory { get; set; }
        public Store Store { get; set; }
        public List<ProductImage>? ProductImages { get; set; }
        public List<Cart>? Cart { get; set; }
        public List<Order>? Orders { get; set; }
    }
}
