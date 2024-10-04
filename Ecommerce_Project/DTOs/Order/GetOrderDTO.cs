using Ecommerce_Project.Models;

namespace Ecommerce_Project.DTOs.Order
{
    public class GetOrderDTO
    {
        public int Id { get; set; }
        public string OrderID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public long Quantity { get; set; }
        public decimal Total { get; set; }
        public decimal Payment { get; set; }

        // Foreign keys
        public Models.Product Product { get; set; }
        public Models.ApplicationUser ApplicationUser { get; set; }
        public Models.Country Country { get; set; }
        public Models.PaymentMode PaymentMode { get; set; }
    }
}
