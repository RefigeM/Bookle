using Bookle.Core.Entities.Common;

namespace Bookle.Core.Entities;

public class Review :BaseEntity
{
	public int BookId { get; set; }
	public Book Book { get; set; }

	public int UserId { get; set; }
	public string Username { get; set; }

	public int Rating { get; set; }
	public string Comment { get; set; }
}
