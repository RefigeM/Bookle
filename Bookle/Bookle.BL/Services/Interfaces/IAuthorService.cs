using Bookle.Core.Entities;

namespace Bookle.BL.Services.Interfaces;

public interface IAuthorService
{
	Task<IEnumerable<Author>> GetAllAuthorsAsync();	
	Task<Author> GetAuthorById(int id);	
	Task AddAuthorAsync(Author author);	
	Task UpdateAuthorAsync(int id,Author author);	
	Task DeleteAuthorAsync(int id);
	Task RestoreAuthorAsync(int id);
	Task SoftDeleteAuthorAsync(int id);
}
