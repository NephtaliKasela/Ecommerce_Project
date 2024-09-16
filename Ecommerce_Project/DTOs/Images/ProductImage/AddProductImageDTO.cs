namespace Ecommerce_Project.DTOs.Images.ProductImage
{
    public class AddProductImageDTO
    {
        public int ProductId { get; set; }
        public Models.Product Product { get; set; }
        public List<IFormFile> files { get; set; }
    }
}
