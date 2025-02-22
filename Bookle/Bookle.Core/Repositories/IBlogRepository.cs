using Bookle.Core.Entities;

namespace Bookle.Core.Repositories;

public interface IBlogRepository :IGenericRepository<Blog>
{
    Task<IEnumerable<Blog>> GetAllRecentPostsAsync();
    Task ToggleIsVisible(int id);


}
