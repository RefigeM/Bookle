using Bookle.Core.Entities;


namespace Bookle.Core.Repositories;

public interface IAuthorRepository : IGenericRepository<Author>
{
    IQueryable<Author> GetAllAuthorsWithDetailsAsync();
	Task<Author> GetAuthorDetailsWithIdAsync(int id);
	Task<IEnumerable<Author>> SearchByAuthorAsync(string name);


}
