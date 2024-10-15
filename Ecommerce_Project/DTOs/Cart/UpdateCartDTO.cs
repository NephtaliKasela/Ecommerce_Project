using Ecommerce_Project.Models;

namespace Ecommerce_Project.DTOs.Cart
{
	public class UpdateCartDTO
	{
		public int Id { get; set; }
		public long Quantity { get; set; }
		public decimal Total { get; set; }
		public bool Complete { get; set; }

		// Foreign keys
		public ApplicationUser ApplicationUser { get; set; }
		public Models.Product Product { get; set; }
	}
}
