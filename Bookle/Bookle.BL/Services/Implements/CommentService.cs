using Bookle.BL.Exceptions;
using Bookle.BL.Services.Interfaces;
using Bookle.BL.ViewModels.BookVMs;
using Bookle.Core.Entities;
using Bookle.Core.Repositories;
using Bookle.DAL.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Bookle.BL.Services.Implements;

public class CommentService(BookleDbContext _context, ICommentRepository _repo) : ICommentService
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

	public async Task AddCommentAsync(Comment comment)
	{
		if (comment == null) throw new NotFoundException("comment is null");

		await _repo.AddAsync(comment);
		await _repo.SaveAsync();
	}

	public async Task DeleteCommentAsync(int id)
	{
		var comment =await _repo.GetByIdAsync(id);
		if (comment == null) throw new NotFoundException();
		_repo.DeleteAsync(id);
		await _repo.SaveAsync();	

	}

	public async Task<List<Comment>> GetAllCommentsAsync()
	{
		var comments = await _repo.GetAllAsync();	
		if (comments.Count == 0) throw new NotFoundException();
		return comments;
	}

	public async Task<Comment> GetCommentByIdAsync(int id)
	{
		var comment= await _repo.GetByIdAsync(id);
		if(comment == null) throw new NotFoundException();	
		return comment;	
	}

	public List<Comment> GetCommentsByBookId(int bookId)
	{
		return _context.Comments
					.Where(c => c.BookId == bookId)
					.Include(c => c.User) // İstifadəçi məlumatlarını da çəkirik
					.OrderByDescending(c => c.CreatedDate)
					.ToList();
	}

	public async Task<List<Comment>> GetCommentWithBookAndUser()
	{
		return await _repo.GetCommentWithBookAndUser();
	}

	public Task RestoreCommentAsync(int id)
	{
		throw new NotImplementedException();
	}

	public Task SoftDeleteCommentAsync(int id)
	{
		throw new NotImplementedException();
	}

	public Task UpdateCommentAsync(int id, BookUpdateVM vm)
	{
		throw new NotImplementedException();
	}

	

	
}
