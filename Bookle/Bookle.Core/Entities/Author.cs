using Bookle.Core.Entities.Common;

namespace Bookle.Core.Entities;

public class Author :BaseEntity
{
	public string AuthorName { get; set; }
	public string? AuthorImage { get; set; }
	public ICollection<Book>? Books { get; set; }
	public bool IsFeatured { get; set; } = false;


}
