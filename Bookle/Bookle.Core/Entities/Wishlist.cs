using Bookle.Core.Entities.Common;

namespace Bookle.Core.Entities
{
	public class Wishlist :BaseEntity
	{
		public string UserId { get; set; } 
		public int BookId { get; set; }

		public User User { get; set; }
		public Book Book { get; set; }
		public bool IsInWishlist { get; set; }
	}
}
