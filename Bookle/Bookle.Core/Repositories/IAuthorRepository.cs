using Bookle.Core.Entities;


namespace Bookle.Core.Repositories;

public interface IAuthorRepository : IGenericRepository<Author>
{
	Task<List<Author>> GetAllAuthorsWithDetailsAsync();
	Task<Author> GetAuthorDetailsWithIdAsync(int id);
	Task<IEnumerable<Author>> SearchByAuthorAsync(string name);


}
