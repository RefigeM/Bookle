using Bookle.Core.Entities;
using Bookle.Core.Repositories;

namespace Bookle.BL.Services.Interfaces;

public interface IBookService
{
	Task<IEnumerable<Book>> GetAllBooksAsync();
	Task<Book> GetBookById(int id);	
	Task AddBookAsync(Book book);	
	Task UpdateBookAsync(int id,Book book);	
	Task DeleteBookAsync(int id);		
}
