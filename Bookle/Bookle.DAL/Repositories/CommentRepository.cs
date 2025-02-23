using Bookle.Core.Entities;
using Bookle.Core.Repositories;
using Bookle.DAL.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Bookle.DAL.Repositories
{
	public class CommentRepository : GenericRepository<Comment>, ICommentRepository
	{
		private readonly BookleDbContext _context;
		public CommentRepository(BookleDbContext context) : base(context)
		{
			_context = context;

		}

		public Task<List<Comment>> GetAllCommentsWithDetailsAsync()
		{
			var comment = _context.Comments
					.Include(c => c.Book)
			.ThenInclude(b => b.Author)
			.Include(c => c.Book)
			.ThenInclude(b => b.BookRatings)
			.Include(c => c.User)
			.ToListAsync();
			return comment;

		}

		public async Task<Comment> GetCommentWithBookUserAuthorWithIdAsync(int id)
		{
			var comment = await _context.Comments
		.Include(c => c.Book)
			.ThenInclude(b => b.BookRatings)
		.Include(c => c.Book)
			.ThenInclude(b => b.Author) 
		.Include(c => c.User)
		.FirstOrDefaultAsync(c => c.Id == id);


			return comment;
		}
		public IQueryable<Comment> GetAllCommentsWithDetails()
		{
			return _context.Comments
				.Include(c => c.User)  
				.Include(c => c.Book)  
				.AsQueryable();
		}

	}
}
