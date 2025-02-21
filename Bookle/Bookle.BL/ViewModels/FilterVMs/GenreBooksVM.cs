using Bookle.Core.Entities;
using Bookle.Core.Enums;

namespace Bookle.BL.ViewModels.FilterVMs
{
    public class GenreBooksVM
    {
        public List<Genre> Genres { get; set; } = new List<Genre>();
        public List<Book> Books { get; set; } = new List<Book>();
        public Genre? SelectedGenre { get; set; } // Seçilən janr

    }
}
