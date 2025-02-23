using Bookle.Core.Entities;

namespace Bookle.Core.Repositories;

public interface IBlogRepository :IGenericRepository<Blog>
{
    Task<IEnumerable<Blog>> GetAllRecentPostsAsync();
    Task<IEnumerable<Blog>> GetAllPostsVisiblePostsAsync();
    //Task ToggleIsVisible(int id);


}
