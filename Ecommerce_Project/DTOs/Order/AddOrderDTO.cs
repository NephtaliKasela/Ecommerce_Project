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
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public long Quantity { get; set; }
        public decimal Amount { get; set; }  // Amount
        //public decimal Payment { get; set; } // bool or payment id
        public string Status { get; set; } // Pending, Process, Success, Cancel ...

        // Foreign keys
        public ApplicationUser ApplicationUser { get; set; }
        public string CountryId { get; set; }
        public string PaymentModeId { get; set; }
        public int CartId { get; set; }
    }
}
