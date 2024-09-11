using Ecommerce_Project.Models.Prices;

namespace Ecommerce_Project.DTOs.Product
{
    public class GetProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ShortDescription { get; set; } = string.Empty;
        public string LongDescription { get; set; } = string.Empty;
        public double Price { get; set; }

        public string Brand { get; set; } = string.Empty;
        public string MadeIn { get; set; } = string.Empty;

        public long Stock { get; set; }     // How many pieces
        public DateTime PublicationDate { get; set; }

        // Foreign Keys
        public List<ProductPrice>? Prices { get; set; }
        public Models.Subcategory Subcategory { get; set; }
        public Models.Store Store { get; set; }
        public List<Models.Images.ProductImage>? BodyProductImages { get; set; }
    }
}
