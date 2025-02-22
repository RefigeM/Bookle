using Bookle.BL.ViewModels.AuthorVMs;
using Bookle.Core.Entities;

namespace Bookle.BL.Services.Interfaces;

public interface IAuthorService
{
	Task<IEnumerable<Author>> GetAllAuthorsAsync();	
	Task<Author?> GetAuthorById(int id);	
	Task AddAuthorAsync(Author author);	
	Task DeleteAuthorAsync(int id);
	Task RestoreAuthorAsync(int id);
	Task SoftDeleteAuthorAsync(int id);
	Task UpdateAuthorAsync(int id,AuthorUpdateVM vm);
	//Task<List<BookCountOfAuthor?>> GetBookCountOfAuthor(int? id);
	Task<List<Author>> GetAllAuthorsWithBooksAsync();
	Task<AuthorAllDataVM?> GetAuthorDetailsWithIdAsync(int id);
	Task<List<AuthorAllDataVM>> GetAllAuthorProfilesAsync();
	Task<List<AuthorAllDataVM>> GetAllFeaturedAuthorProfilesAsync();
	Task ToggleAuthorIsFeaturedAsync(int id);
	Task<List<BookCountOfAuthor>>  GetAuthorsWithBookCounts();
	Task<AuthorAllDataVM?> GetAuthorWithBooksAsync(int authorId);




}
