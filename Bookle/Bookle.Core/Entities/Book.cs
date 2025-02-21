using Bookle.Core.Entities.Common;
using Bookle.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Bookle.Core.Entities;

public class Book : BaseEntity
{
	public string Title { get; set; }
	public int AuthorId { get; set; }
	public Author Author { get; set; }
	public bool IsFeatured { get; set; } = false;

    [MaxLength(500, ErrorMessage = "ShortDescription can have a maximum of 500 characters.")]

    public string? ShortDescription { get; set; }

    [MaxLength(1000, ErrorMessage = "Description can have a maximum of 1000 characters.")]

    public string? Description { get; set; }

    [MaxLength(2000, ErrorMessage = "RoleOfBook can have a maximum of 2000 characters.")]
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
