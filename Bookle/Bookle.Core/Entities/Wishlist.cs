using Bookle.Core.Entities.Common;

namespace Bookle.Core.Entities
{
	public class Wishlist :BaseEntity
	{
		public string UserId { get; set; } // Hər bir user-ə bağlı olmaq üçün
		public int BookId { get; set; }

		public User User { get; set; }
		public Book Book { get; set; }
	}
}
