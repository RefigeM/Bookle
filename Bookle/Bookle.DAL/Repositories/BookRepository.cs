using Bookle.Core.Entities;
using Bookle.Core.Enums;
using Bookle.Core.Repositories;
using Bookle.DAL.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Bookle.DAL.Repositories;

public class BookRepository : GenericRepository<Book>, IBookRepository
{
	private readonly BookleDbContext _context;


	public BookRepository(BookleDbContext context) : base(context)
	{
		_context = context;
	}

    public IEnumerable<Genre> GetAllGenres()
    {
        return Enum.GetValues(typeof(Genre)).Cast<Genre>().ToList();
    }

    public async Task<IEnumerable<Book>> GetAllWithDetailsAsync()
	{
		return await _context.Books
			.Include(b => b.Author)
			//.Include(b => b.Read)
			.Include(b => b.BookRatings)
				.ToListAsync();
	}

   public  IEnumerable<Book> GetBooksByGenre(Genre? genre)
    {
        var books = _context.Books.AsQueryable();

        if (genre.HasValue)
        {
            books = books.Where(b => b.Genre == genre.Value);
        }

        return books.ToList(); // Bura diqqət et
    }


    public async Task<Book> GetByIdWithDetailsAsync(int id)
	{
		return await _context.Books
	   .Include(b => b.Author)
	   .FirstOrDefaultAsync(b => b.Id == id);

	}

    public async Task<List<Book>> GetTopRatedBooksAsync(int count)
    {
        return await _context.Books
          .OrderByDescending(b => b.BookRatings.Any() ? b.BookRatings.Average(r => r.RatingRate) : 0)
          .Take(count)  
          .ToListAsync();
    }

    public IEnumerable<Book> Search(string query)
    {
        var books = _context.Books
        .Include(b => b.Author)
        .Include(b => b.BookRatings)
        .Include(b => b.Comments) 
        .Where(b => b.Title.Contains(query) || (b.Author != null && b.Author.AuthorName.Contains(query)))
        .ToList();

        return books;
    }

  
}
