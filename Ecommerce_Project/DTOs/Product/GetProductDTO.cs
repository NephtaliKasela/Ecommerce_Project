﻿using Bogus.DataSets;
using Ecommerce_Project.DTOs.Images.ProductImage;
using Ecommerce_Project.Models.Images;
using Ecommerce_Project.Models.Prices;

namespace Ecommerce_Project.DTOs.Product
{
    public class GetProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal SoldPrice { get; set; }

        public string Brand { get; set; } = string.Empty;
        public string MadeIn { get; set; } = string.Empty;

        public long StockQuantity { get; set; }     // How many pieces
        public int MinimumOrder { get; set; }
        public string SerialNumber { get; set; } = string.Empty;

        public DateTime PublicationDate { get; set; }

        // Foreign Keys
        public List<ProductPrice>? Prices { get; set; }
        public Models.Subcategory Subcategory { get; set; }
        public Models.Store Store { get; set; }
        public List<ProductImage>? ProductImages { get; set; }
    }
}
