using Bookle.BL.Exceptions;
using Bookle.BL.Services.Interfaces;
using Bookle.BL.ViewModels.AuthorVMs;
using Bookle.Core.Entities;
using Bookle.Core.Repositories;
using Bookle.DAL.Contexts;

namespace Bookle.BL.Services.Implements;

public class BookService(IBookRepository _repo, BookleDbContext _context) : IBookService
{
	public async Task AddBookAsync(Book book)
	{
		if (book == null) throw new NotFoundException("Book is null");
		await _repo.AddAsync(book);
		await _repo.SaveAsync();
	}

	public async Task DeleteBookAsync(int id)
	{
		var book = await _repo.GetByIdAsync(id);
		if (book == null) throw new NotFoundException("Book is null");
		await _repo.DeleteAsync(id);
		await _repo.SaveAsync();
	}

	public async Task<IEnumerable<Book>> GetAllBooksAsync()
	{
		var data = await _repo.GetAllWithDetailsAsync();
		if (data == null) throw new NotFoundException();
		return data;
	}

	public async Task<Book> GetBookByIdAsync(int id)
	{
		var book = await _repo.GetByIdWithDetailsAsync(id);
		if (book == null) throw new NotFoundException();
		return book;

	}

	public async Task RestoreBookAsync(int id)
	{
		await _repo.RestoreAsync(id);
		await _repo.SaveAsync();
	}

	public async Task SoftDeleteBookAsync(int id)
	{
		await _repo.SoftDeleteAsync(id);
		await _repo.SaveAsync();
	}

	public Task UpdateBookAsync(int id, AuthorUpdateVM vm)
	{
		throw new NotImplementedException();
	}
}
