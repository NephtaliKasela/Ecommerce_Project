namespace Ecommerce_Project.Models.Images
{
    public class ProductImage
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public byte[] ImageData { get; set; }
        public string ContentType { get; set; }
        public Product BodyProduct { get; set; }
    }
}
