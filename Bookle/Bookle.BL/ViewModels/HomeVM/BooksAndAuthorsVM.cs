using Bookle.BL.ViewModels.AuthorVMs;
using Bookle.Core.Entities;
using Bookle.Core.Enums;

namespace Bookle.BL.ViewModels.HomeVM
{
	public class BooksAndAuthorsVM
	{
		public List<Book> Books { get; set; } = new List<Book>(); 
		public List<AuthorAllDataVM> Authors { get; set; } = new List<AuthorAllDataVM>();
		public bool IsInWishlist { get; set; }
        public List<Book> TopRatedBooks { get; set; } = new();
        public List<Genre> Genres { get; set; } = new List<Genre>();
        public Genre? SelectedGenre { get; set; } // Seçilən janr

    }
}
