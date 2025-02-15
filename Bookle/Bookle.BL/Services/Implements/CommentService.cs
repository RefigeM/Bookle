using Bookle.BL.Services.Interfaces;
using Bookle.Core.Entities;
using Bookle.DAL.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Bookle.BL.Services.Implements;

public class CommentService(BookleDbContext _context) : ICommentService
{
	public void AddComment(int bookId, string userId, string content)
	{
		var comment = new Comment
		{
			BookId = bookId,
			UserId = userId,
			Content = content,
			CreatedDate = DateTime.Now
		};

		_context.Comments.Add(comment);
		_context.SaveChanges();
	}

	public List<Comment> GetCommentsByBookId(int bookId)
	{
		return _context.Comments
					.Where(c => c.BookId == bookId)
					.Include(c => c.User) // İstifadəçi məlumatlarını da çəkirik
					.OrderByDescending(c => c.CreatedDate)
					.ToList();
	}
}
