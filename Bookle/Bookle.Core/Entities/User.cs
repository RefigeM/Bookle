using Microsoft.AspNetCore.Identity;

namespace Bookle.Core.Entities;

public class User : IdentityUser
{
	public bool IsDeleted { get; set; } = false;
	public string Fullname { get; set; }
	public string? Address { get; set; }
	public string? ProfilImage { get; set; }
	public ICollection<BookRating>? BookRatings { get; set; }
	public ICollection<Comment>? Comments { get; set; }


}
