using Microsoft.AspNetCore.Identity;

namespace Bookle.Core.Entities;

public class User : IdentityUser
{
	public string Fullname { get; set; }
	public string? Address { get; set; }
	public string? ProfilImage { get; set; }

}
