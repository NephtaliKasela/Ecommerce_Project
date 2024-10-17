namespace Ecommerce_Project.Models
{
    public class Order
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
        //public decimal Price { get; set; }
        //public decimal Discount { get; set; }
        public long Quantity { get; set; }
        public decimal Total { get; set; }  // Amount
        public decimal Payment { get; set; } // bool

        // Foreign keys
        public Product Product { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public Country Country { get; set; }
        public PaymentMode PaymentMode { get; set; }
    }
}
