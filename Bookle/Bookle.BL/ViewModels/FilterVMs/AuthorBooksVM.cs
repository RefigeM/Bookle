using Bookle.Core.Entities;

namespace Bookle.BL.ViewModels.FilterVMs
{
  public  class AuthorBooksVM
    {
        public List<Book> Books { get; set; }
        public List<Author> Authors { get; set; }
    }
}
