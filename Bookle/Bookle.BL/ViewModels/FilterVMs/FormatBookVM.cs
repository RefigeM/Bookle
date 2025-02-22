using Bookle.Core.Entities;
using Bookle.Core.Enums;

namespace Bookle.BL.ViewModels.FilterVMs;

public class FormatBookVM
{
    public List<Format> Formats { get; set; } = new List<Format>();
    public List<Book> Books { get; set; } = new List<Book>();
    public Format? SelectedFormat { get; set; }
}
