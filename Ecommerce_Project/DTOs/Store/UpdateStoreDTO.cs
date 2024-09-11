using Ecommerce_Project.Models;

namespace Ecommerce_Project.DTOs.Store
{
    public class UpdateStoreDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        // Store Owner info
        public string StoreContactInfo { get; set; } = string.Empty;
        //public string StoreOpeningHours { get; set; } = string.Empty;
        //public string StoreClosingHours { get; set; } = string.Empty;
        public string StoreLogoUrl { get; set; } = string.Empty;
        public string StoreWebsite { get; set; } = string.Empty;
        public string StoreEmail { get; set; } = string.Empty;
        public string StoreFacebookUrl { get; set; } = string.Empty;
        public string StoreTwitterUrl { get; set; } = string.Empty;
        public string StoreInstagramUrl { get; set; } = string.Empty;
        public string StoreShippingPolicy { get; set; } = string.Empty;
        public string StoreReturnPolicy { get; set; } = string.Empty;
        public bool StoreFeatured { get; set; }
        public bool StoreActive { get; set; }
        public DateTime StoreCreatedAt { get; set; }
        public DateTime StoreUpdatedAt { get; set; }

        // Foreign Keys
        public string CountryId { get; set; }
        public string CityId { get; set; }
        //public List<BodyProduct>? BodyProducts { get; set; }
        //public StoreLocation? Location { get; set; }
    }
}
