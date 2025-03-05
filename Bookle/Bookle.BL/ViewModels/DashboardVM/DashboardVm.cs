using Bookle.Core.Entities;

namespace Bookle.BL.ViewModels.DashboardVM;

public class DashboardVm
{
	public List<Book> Books { get; set; }
	public List<Author> Authors { get; set; }
	public List<Blog> Blogs { get; set; }
	public List<User> Users { get; set; }
	public List<Book> TopRatedBooks { get; set; } = new();
	public int TotalBookCount { get; set; }
	public int TotalUserCount { get; set; }
	public int TotalBlogCount { get; set; }
	public int TotalAuthorCount { get; set; }



}
