using Bookle.BL.ViewModels.AuthorVMs;
using Bookle.Core.Entities;

namespace Bookle.BL.ViewModels.HomeVM
{
	public class BooksAndAuthorsVM
	{
		public List<Book> Books { get; set; } = new List<Book>(); 
		public List<AuthorAllDataVM> Authors { get; set; } = new List<AuthorAllDataVM>();
		public bool IsInWishlist { get; set; }

	}
}
