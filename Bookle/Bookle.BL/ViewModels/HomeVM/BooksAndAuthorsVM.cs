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
        public Genre? SelectedGenre { get; set; } 
        public List<Comment> Comments { get; set; } = new List<Comment>();
        public List<User> Users { get; set; } = new List<User>();
        public List<BookCountOfAuthor> AuthorsWithBookCounts { get; set; } = new List<BookCountOfAuthor>();



    }
}
