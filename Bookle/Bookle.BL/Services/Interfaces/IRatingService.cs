namespace Bookle.BL.Services.Interfaces;

public interface IRatingService
{
	void AddRating(int bookId, string userId, int star);
	double GetAverageRating(int bookId);

}
