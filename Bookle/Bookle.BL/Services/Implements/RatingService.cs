//using Bookle.BL.Services.Interfaces;
//using Bookle.DAL.Contexts;
//using Microsoft.EntityFrameworkCore;

//namespace Bookle.BL.Services.Implements;

//public class RatingService(BookleDbContext _context) : IRatingService
//{
//	public void AddRating(int bookId, string userId, int star)
//	{
//		var existingRating = _context.BookRatings
//		   .FirstOrDefault(r => r.BookId == bookId && r.UserId == userId);

//		if (existingRating != null)
//		{
//			existingRating.RatingRate = star; // Mövcud reytinqi yenilə
//		}
//		else
//		{
//			var rating = new BookRatings
//			{
//				BookId = bookId,
//				UserId = userId,
//				Score = star,
//				CreatedAt = DateTime.Now
//			};
//			_context.BookRatings.Add(rating);
//		}

//		_context.SaveChanges();
//	}
//}
