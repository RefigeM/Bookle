using Bookle.BL.Services.Interfaces;
using Bookle.Core.Entities;
using Bookle.Core.Repositories;

namespace Bookle.BL.Services.Implements;

public class BookService(IBookRepository _repo) : IBookService
{
	public async Task AddBookAsync(Book book)
	{
	await _repo.AddAsync(book);	
	}

	public Task DeleteBookAsync(int id)
	{
		throw new NotImplementedException();
	}

	public Task<IEnumerable<Book>> GetAllBooksAsync()
	{
		throw new NotImplementedException();
	}

	public Task<Book> GetBookById(int id)
	{
		throw new NotImplementedException();
	}

	public Task UpdateBookAsync(int id, Book book)
	{
		throw new NotImplementedException();
	}
}
