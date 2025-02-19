using Bookle.Core.Entities;

namespace Bookle.BL.ViewModels.WishlistVMs
{
	public class WishlistCookieItemVM
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public int BookId { get; set; }
		public string Title { get; set; }
		public Book Book { get; set; }
	}
}
