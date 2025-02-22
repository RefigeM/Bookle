using Bookle.BL.ViewModels.BlogVMs;
using Bookle.Core.Entities;

namespace Bookle.BL.Services.Interfaces;

public interface IBlogService
{
    Task<IEnumerable<Blog>> GetAllRecentPostsAsync();
    Task<Blog> GetBlogByIdAsync(int id);
    Task AddPostAsync(Blog post);
    Task UpdatePostAsync(int id,BlogUpdateVM vm);
    Task DeletePostAsync(int id);
	Task RestoreBlogAsync(int id);
	Task SoftDeketeAsync(int id);
    Task ToggleIsVisible(int id);

}
