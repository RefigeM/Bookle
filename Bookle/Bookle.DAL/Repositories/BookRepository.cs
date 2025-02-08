using Bookle.Core.Entities;
using Bookle.Core.Repositories;
using Bookle.DAL.Contexts;

namespace Bookle.DAL.Repositories;

public class BookRepository : GenericRepository<Book>, IBookRepository
{
	public BookRepository(BookleDbContext _context) : base(_context)
	{
	}
}
