using Bookle.BL.Exceptions;
using Bookle.BL.Services.Interfaces;
using Bookle.Core.Entities;
using Bookle.Core.Repositories;

namespace Bookle.BL.Services.Implements;

public class ReviewService(ICommentRepository _repo) : IReviewService
{
	public async Task DeleteReviewAsync(int id)
	{
		var review= await _repo.GetByIdAsync(id);
		if (review == null) throw new NotFoundException();
		_repo.DeleteAsync(id);
		await _repo.SaveAsync();
	}
}
