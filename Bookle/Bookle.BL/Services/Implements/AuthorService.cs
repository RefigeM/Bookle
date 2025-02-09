using Bookle.BL.Exceptions;
using Bookle.BL.Extentions;
using Bookle.BL.Services.Interfaces;
using Bookle.BL.ViewModels.AuthorVMs;
using Bookle.Core.Entities;
using Bookle.Core.Repositories;
using Bookle.DAL.Contexts;

namespace Bookle.BL.Services.Implements;

public class AuthorService(IAuthorRepository _repo, BookleDbContext _context) : IAuthorService
{
	public async Task AddAuthorAsync(Author author)
	{
		if (author == null) throw new NotFoundException("author is null");

		await _repo.AddAsync(author);
		await _repo.SaveAsync();
	}

	public async Task DeleteAuthorAsync(int id)
	{
		var author = await _repo.GetByIdAsync(id);
		if (author == null) throw new NotFoundException();
		await _repo.DeleteAsync(id);
		await _repo.SaveAsync();
	}

	public async Task<IEnumerable<Author>> GetAllAuthorsAsync()
	{
		return await _repo.GetAllAsync();
	}

	public async Task<Author> GetAuthorById(int id)
	{
		var author = await _repo.GetByIdAsync(id);
		if (author == null) throw new NotFoundException();
		return author;
	}

	public async Task RestoreAuthorAsync(int id)
	{
		await _repo.RestoreAsync(id);
		await _repo.SaveAsync();
	}

	public async Task SoftDeleteAuthorAsync(int id)
	{
		await _repo.SoftDeleteAsync(id);
		await _repo.SaveAsync();
	}

	public async Task UpdateAuthorAsync(int id, AuthorUpdateVM vm)
	{
		var author = await _repo.GetByIdAsync(id);
		if (author == null) throw new NotFoundException();

		author.AuthorName = vm.AuthorName;
		if (vm.File != null)
		{
			string newFileName = await vm.File.UploadAsync("wwwroot/imgs/authors");
			author.AuthorImage = "/imgs/authors/" + newFileName;
		}
		await _repo.SaveAsync();
	}

	
}
