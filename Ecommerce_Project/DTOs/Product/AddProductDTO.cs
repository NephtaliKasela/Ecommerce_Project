using Ecommerce_Project.Models.Prices;

namespace Ecommerce_Project.DTOs.Product
{
    public class AddProductDTO
    {
        public string Name { get; set; } = string.Empty;
        public string ShortDescription { get; set; } = string.Empty;
        public string LongDescription { get; set; } = string.Empty;
        public double Price { get; set; }
        public decimal SoldPrice { get; set; }

        public string Brand { get; set; } = string.Empty;
        public string MadeIn { get; set; } = string.Empty;

        public long Stock { get; set; }     // How many pieces
        public DateTime PublicationDate { get; set; }

        // Foreign Keys
        public string SubcategoryId { get; set; }
        public string StoreId { get; set; }
        //public List<BodyProductPrice>? Prices { get; set; }
        //public List<Models.Images.BodyProductImage>? BodyProductImages { get; set; }
    }
}
