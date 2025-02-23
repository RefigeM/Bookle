using Bookle.BL.Exceptions;
using Bookle.BL.Extentions;
using Bookle.BL.Services.Interfaces;
using Bookle.BL.ViewModels.BlogVMs;
using Bookle.Core.Entities;
using Bookle.Core.Repositories;

namespace Bookle.BL.Services.Implements;

public class BlogService : IBlogService
{
	private readonly IBlogRepository _repo;

	public BlogService(IBlogRepository repo)
	{
		_repo = repo;
	}

	public async Task AddPostAsync(Blog post)
	{
		if (post == null) throw new NotFoundException("author is null");

		await _repo.AddAsync(post);
		await _repo.SaveAsync();
	}

	public async Task DeletePostAsync(int id)
	{
		var blog = await _repo.GetByIdAsync(id);
		if (blog == null) throw new NotFoundException();
		_repo.DeleteAsync(id);
		await _repo.SaveAsync();
	}

    public async Task<IEnumerable<Blog>> GetAllPostsVisiblePostsAsync()
    {
        var blogs = await _repo.GetAllRecentPostsAsync();
        if (blogs == null || !blogs.Any())
        {
            return new List<Blog>();
        }
        return blogs;
    }

    public async Task<IEnumerable<Blog>> GetAllRecentPostsAsync()
	{
		var blogs = await _repo.GetAllRecentPostsAsync();
		if (blogs == null || !blogs.Any())
		{
			return new List<Blog>();
		}
		return blogs;
	}

	public async Task<Blog> GetBlogByIdAsync(int id)
	{
		var blog = await _repo.GetByIdAsync(id);
		if (blog == null) throw new NotFoundException();
		return blog;
	}

	public async Task RestoreBlogAsync(int id)
	{
		var blog = _repo.RestoreAsync(id);
		await _repo.SaveAsync();

	}

	public async Task SoftDeketeAsync(int id)
	{
		var blog = _repo.SoftDeleteAsync(id);
		await _repo.SoftDeleteAsync(id);
	}

	public async Task ToggleIsVisible(int id)
	{
		var data = await _repo.GetByIdAsync(id);
		if (data == null) throw new NotFoundException("Book is null");

		data.IsVisibleOnHomepage = !data.IsVisibleOnHomepage;
		await _repo.SaveAsync();

	}

	public async Task UpdatePostAsync(int id, BlogUpdateVM vm)
	{
		var blog = await _repo.GetByIdAsync(id);
		if (blog == null) throw new NotFoundException();
		blog.Title = vm.Title;
		blog.Content = vm.Content;

		if (vm.File != null)
		{
			string newFileName = await vm.File.UploadAsync("wwwroot/imgs/blogs");
			blog.ImageUrl = "/imgs/blogs/" + newFileName;
		}
		await _repo.SaveAsync();
	}
}



