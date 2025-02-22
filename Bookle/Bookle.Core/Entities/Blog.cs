using Bookle.Core.Entities.Common;

namespace Bookle.Core.Entities;

public class Blog :BaseEntity
{
    public string Title { get; set; }  
    public string Content { get; set; }
    public string Author { get; set; } = "Admin";
    public string ImageUrl { get; set; }
    public bool IsVisibleOnHomepage { get; set; } = false;
}
