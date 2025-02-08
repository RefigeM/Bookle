using Bookle.BL.Exceptions;
using Bookle.BL.Services.Interfaces;
using Bookle.Core.Entities;
using Bookle.Core.Repositories;

namespace Bookle.BL.Services.Implements;

public class AuthorService(IAuthorRepository _repo) : IAuthorService
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
		if (author == null) throw new Exception();
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

	public Task UpdateAuthorAsync(int id, Author author)
	{
		throw new NotImplementedException();
	}
}
