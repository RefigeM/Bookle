using Bookle.Core.Entities;
using Bookle.Core.Repositories;
using Bookle.DAL.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Bookle.DAL.Repositories;

public class AuthorRepository : GenericRepository<Author>, IAuthorRepository
{
	private readonly BookleDbContext _context;
	public AuthorRepository(BookleDbContext context) : base(context)
	{
		_context = context;
	}

	public IQueryable<Author> GetAllAuthorsWithDetailsAsync()
	{
		var authors =  _context.Authors
				.Include(a => a.Books);
		return authors;
	}

	public async Task<Author> GetAuthorDetailsWithIdAsync(int id)
	{
		var author = await _context.Authors
				.Include(a => a.Books).FirstOrDefaultAsync(a => a.Id == id);
		return author;

	}

	public async Task<IEnumerable<Author>> SearchByAuthorAsync(string name)
	{
		return await _context.Authors
						   .Where(b => b.AuthorName.Contains(name))
						   .ToListAsync();
	}
}
