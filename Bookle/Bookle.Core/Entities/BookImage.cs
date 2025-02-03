using Bookle.Core.Entities.Common;

namespace Bookle.Core.Entities;

public class BookImage :BaseEntity
{
	public int BookId { get; set; }
	public Book Book { get; set; }
	public string ImageUrl { get; set; }
}
