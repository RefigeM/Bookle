using Bookle.Core.Entities;

namespace Bookle.Core.Repositories
{
	public interface IBookRepository : IGenericRepository<Book>
	{
		Task<Book> GetByIdWithDetailsAsync(int id);
		Task<IEnumerable<Book>> GetAllWithDetailsAsync();
		Task<List<Book>> GetTopRatedBooksAsync(int count);
        IEnumerable<Book> Search(string query);


    }
}
