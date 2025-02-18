using Bookle.Core.Entities;

namespace Bookle.Core.Repositories
{
	public interface ICommentRepository :IGenericRepository<Comment>
	{
		Task<List<Comment>> GetAllCommentsWithDetailsAsync();
		Task<Comment> GetCommentWithBookUserAuthorWithIdAsync(int id);
	}
}
