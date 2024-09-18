using Microsoft.AspNetCore.Identity;

namespace Ecommerce_Project.Models
{
    public class ApplicationUser : IdentityUser
    {
        public Store? Store { get; set; }
    }
}
