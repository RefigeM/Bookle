using Bookle.Core.Entities;

namespace Bookle.Core.Repositories;

public interface IBlogRepository :IGenericRepository<Blog>
{
	IQueryable<Blog> GetAllRecentPostsAsync();
	IQueryable<Blog> GetAllPostsVisiblePostsAsync();
    //Task ToggleIsVisible(int id);


}
