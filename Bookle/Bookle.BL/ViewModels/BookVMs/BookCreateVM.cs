using Bookle.Core.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Bookle.BL.ViewModels.BookVMs;

public class BookCreateVM
{
	[Required]
	[MaxLength(32, ErrorMessage ="32den cox ola bilmez.")]
	public string Title { get; set; }

	[Required]
	public int AuthorId { get; set; }

	[StringLength(500)]
	public string? ShortDescription { get; set; }

	public string? Description { get; set; }

	public string? RoleOfBook { get; set; }

	[Required]
	public Genre Genre { get; set; }

	[Required]
	public Format Format { get; set; }

	[StringLength(13)]
	public string? ISBN { get; set; }

	public string? Country { get; set; }

	[Required]
	[Range(1900, 2100)]
	public int PublishedYear { get; set; }

	[Range(1, 10000)]
	public int PageCount { get; set; }

	[Required]
	[Range(1, 1000)]
	public int Price { get; set; }

	[Required]
	[StringLength(50)]
	public string Language { get; set; }
	[Required(ErrorMessage ="File not selected")]
	public IFormFile CoverImage { get; set; }


	//public string? CoverImageUrl { get; set; }

	public List<string>? Images { get; set; }
}
