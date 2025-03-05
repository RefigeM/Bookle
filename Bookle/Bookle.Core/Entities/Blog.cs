using Bookle.Core.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace Bookle.Core.Entities;

public class Blog :BaseEntity
{
	[Required]
	[MaxLength(64)]
	public string Title { get; set; }
	[Required]
	[MaxLength(4000)]
	public string Content { get; set; }
    public string Author { get; set; } = "Admin";
    public string ImageUrl { get; set; }
    public bool IsVisibleOnHomepage { get; set; } = false;
}
