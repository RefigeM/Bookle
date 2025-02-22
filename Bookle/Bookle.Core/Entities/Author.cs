using Bookle.Core.Entities.Common;

namespace Bookle.Core.Entities;

public class Author :BaseEntity
{
	public string AuthorName { get; set; }
	public string? AuthorImage { get; set; }
	public ICollection<Book>? Books { get; set; }
	public bool IsFeatured { get; set; } = false;
    public string? Biography { get; set; }
    public int? BirthYear { get; set; }  // Doğum ili
    public int? DeathYear { get; set; }  // Ölüm ili
    public string? Country { get; set; }
    public string? FacebookUrl { get; set; }
    public string? TwitterUrl { get; set; }
    public string? InstagramUrl { get; set; }
    public string? LinkedInUrl { get; set; }
}
