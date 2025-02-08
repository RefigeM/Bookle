using Bookle.Core.Entities;
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

	[MaxLength(500)]
	public string? ShortDescription { get; set; }
	[MaxLength(700)]

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
	[Range(1000, 2025)]
	public int PublishedYear { get; set; }

	[Range(1, 10000)]
	public int PageCount { get; set; }

	[Required]
	public int Price { get; set; }

	[Required]
	[MaxLength(50)]
	public string Language { get; set; }

	[Required(ErrorMessage ="File not selected")]
	public IFormFile File { get; set; }
	public string? FileUrl { get; set; }

	//public List<string>? Images { get; set; }
	public ICollection<IFormFile>? OtherFiles { get; set; }
	public static implicit operator Book(BookCreateVM vm) 
	{
		return new Book
		{
			Title = vm.Title,
			AuthorId = vm.AuthorId,
			Language = vm.Language,
			PuslishedYear = vm.PublishedYear,
			Genre = vm.Genre,
			Format = vm.Format,
			Country = vm.Country,
			ISBN = vm.ISBN,
			PageCount = vm.PageCount,
			Price = vm.Price,
			Description = vm.Description,
			ShortDescription = vm.ShortDescription,
			RoleOfBook = vm.RoleOfBook
		};
	}
}
