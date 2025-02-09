using Bookle.BL.ViewModels.AuthorVMs;
using Bookle.Core.Entities;
using Bookle.Core.Repositories;

namespace Bookle.BL.Services.Interfaces;

public interface IBookService
{
	Task<IEnumerable<Book>> GetAllBooksAsync();
	Task<Book> GetBookByIdAsync(int id);
	Task AddBookAsync(Book book);
	Task DeleteBookAsync(int id);
	Task RestoreBookAsync(int id);
	Task SoftDeleteBookAsync(int id);
	Task UpdateBookAsync(int id, AuthorUpdateVM vm);
}
