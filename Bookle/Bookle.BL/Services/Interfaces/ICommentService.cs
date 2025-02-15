using Bookle.Core.Entities;

namespace Bookle.BL.Services.Interfaces;

public interface ICommentService
{
	void AddComment(int bookId, string userId, string content);
	List<Comment> GetCommentsByBookId(int bookId);
}
