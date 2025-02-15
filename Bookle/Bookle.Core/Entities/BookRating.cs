using Bookle.Core.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace Bookle.Core.Entities;

public	class BookRating :BaseEntity
{
	[Range(1,5)]
	public int RatingRate { get; set; }
	public int? BookId { get; set; }
	public Book? Book { get; set; }
	public string? UserId { get; set; }
	public User? User { get; set; }
}
