using Bookle.Core.Entities;
using Bookle.Core.Repositories;
using Bookle.DAL.Contexts;

namespace Bookle.DAL.Repositories;

public class AuthorRepository : GenericRepository<Author>, IAuthorRepository
{
	public AuthorRepository(BookleDbContext _context) : base(_context)
	{
	}
}
