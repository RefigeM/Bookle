using Bookle.Core.Entities.Common;
using Bookle.Core.Enums;

namespace Bookle.Core.Entities;

public class Book :BaseEntity
{
	public string Title { get; set; }
	public string Author { get; set; }
	public Genre Genre { get; set; }
	public string? ISBN { get; set; }

	public DateTime PublishedDate { get; set; }
	public string? Description { get; set; }
	public string? CoverImageUrl { get; set; }
	public ICollection<BookImage> Images { get; set; }


}
