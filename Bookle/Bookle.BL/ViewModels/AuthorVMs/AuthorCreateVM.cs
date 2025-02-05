using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Bookle.BL.ViewModels.AuthorVMs;

public class AuthorCreateVM
{
	[Required]
	[MaxLength(32, ErrorMessage ="Fulname 32den cox ola bilmez")]
	public string AuthorName { get; set; }
	[Required]
	public IFormFile File { get; set; }
	[Required]
	public string imgUrl { get; set; }

}
