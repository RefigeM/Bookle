using Bookle.Core.Entities.Common;
using Bookle.Core.Enums;

namespace Bookle.Core.Entities;

public class Book : BaseEntity
{
	public string Title { get; set; }
	public int AuthorId { get; set; }
	public Author Author { get; set; }
	public bool IsFeatured { get; set; } = false;
	public string? ShortDescription { get; set; }
	public string? Description { get; set; }
	public string? RoleOfBook { get; set; }
	public Genre Genre { get; set; }
	public Format Format { get; set; }
	public string? ISBN { get; set; }
	public string? PublishingCountry { get; set; }
	public int PublishedYear { get; set; }
	public int PageCount { get; set; }
	public decimal Price { get; set; }
	public string Language { get; set; }
	public string? CoverImageUrl { get; set; }
	public ICollection<BookRating>? BookRatings { get; set; }
	public ICollection<Comment>? Comments { get; set; }
	public ICollection<Wishlist> Wishlist { get; set; }
	public bool IsInWishlist { get; set; }


}
