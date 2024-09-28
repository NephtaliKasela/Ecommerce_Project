using Ecommerce_Project.Models;

namespace Ecommerce_Project.DTOs.Order
{
    public class AddOrderDTO
    {
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
        public decimal Total { get; set; }
        public decimal Payment { get; set; }

        // Foreign keys
        public string ProductId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string CountryId { get; set; }
        public string PaymentModeId { get; set; }
    }
}
