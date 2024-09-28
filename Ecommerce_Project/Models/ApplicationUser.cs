using Microsoft.AspNetCore.Identity;

namespace Ecommerce_Project.Models
{
    public class ApplicationUser : IdentityUser
    {
        public Store? Store { get; set; }
        public List<Cart>? Cart { get; set; }
        public List<Order>? Orders { get; set; }
    }
}
