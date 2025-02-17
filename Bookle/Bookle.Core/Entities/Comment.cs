using Bookle.Core.Entities.Common;

namespace Bookle.Core.Entities;

	public class Comment :BaseEntity
{
	public int Id { get; set; }
	public bool IsApproved { get; set; } = true;
	public int BookId { get; set; }
	public Book? Book { get; set; }

	public string UserId { get; set; } = null!;
	public User User { get; set; }
	public string Content { get; set; } = null!;

}
