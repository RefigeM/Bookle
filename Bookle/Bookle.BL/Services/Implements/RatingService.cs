using Bookle.BL.Services.Interfaces;
using Bookle.Core.Entities;
using Bookle.DAL.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Bookle.BL.Services.Implements;

public class RatingService(BookleDbContext _context) : IRatingService
{
	public void AddRating(int bookId, string userId, int star)
	{
		var existingRating = _context.BookRatings
		   .FirstOrDefault(r => r.BookId == bookId && r.UserId == userId);

		if (existingRating != null)
		{
			existingRating.RatingRate = star; // Mövcud reytinqi yenilə
		}
		else
		{
			var rating = new BookRating
			{
				BookId = bookId,
				UserId = userId,
				RatingRate = star,
				CreatedDate = DateTime.Now
			};
			_context.BookRatings.Add(rating);
		}

		_context.SaveChanges();
	}

	public double GetAverageRating(int bookId)
	{
		var ratings = _context.BookRatings
			.Where(r => r.BookId == bookId)
			.ToList();

		if (ratings.Any())
		{
			return ratings.Average(r => r.RatingRate);
		}

		return 0; // Əgər heç bir reytinq yoxdursa, 0 qaytarırıq
	}
}
