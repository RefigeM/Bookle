using Bookle.Core.Entities;
using Bookle.DAL.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

[Authorize] 
public class WishlistController : Controller
{
	private readonly BookleDbContext _context;
	private readonly UserManager<User> _userManager;

	public WishlistController(BookleDbContext context, UserManager<User> userManager)
	{
		_context = context;
		_userManager = userManager;
	}

	public async Task<IActionResult> Index()
	{
		var user = await _userManager.GetUserAsync(User);
		var wishlist = _context.Wishlists
			.Where(w => w.UserId == user.Id)
			.Include(w => w.Book)
			.ThenInclude(b => b.Author)
			.ToList();

		return View(wishlist);
	}

	public async Task<IActionResult> AddToWishlist(int bookId)
	{
		var user = await _userManager.GetUserAsync(User);

		var book = await _context.Books.FindAsync(bookId);
		if (book == null)
		{
			return NotFound("Kitab tapılmadı.");
		}

		var exists = await _context.Wishlists.AnyAsync(w => w.UserId == user.Id && w.BookId == bookId);
		if (exists)
		{
			return BadRequest("Bu kitab artıq wishlist-də var.");
		}

		var wishlist = new Wishlist
		{
			UserId = user.Id,
			BookId = bookId
		};

		_context.Wishlists.Add(wishlist);
		await _context.SaveChangesAsync();

		return RedirectToAction("Index", "Home");
	}


	public async Task<IActionResult> RemoveFromWishlist(int bookId)
	{
		_context.ChangeTracker.Clear(); // Yeni məlumatlarla işləmək üçün

		var user = await _userManager.GetUserAsync(User);
		if (user == null) return RedirectToAction("Login", "Account");

		var wishlistItem = await _context.Wishlists
			.FirstOrDefaultAsync(w => w.UserId == user.Id && w.BookId == bookId);

		if (wishlistItem == null)
		{
			TempData["Message"] = "Bu kitab artıq wishlist-də yoxdur!";
			return RedirectToAction("Index", "Wishlist");
		}

		_context.Wishlists.Remove(wishlistItem);
		await _context.SaveChangesAsync();

		return RedirectToAction("Index", "Home");
	}

	public async Task<IActionResult> WishlistCount()
	{
		var user = await _userManager.GetUserAsync(User);
		if (user == null) return Json(0);

		var count = await _context.Wishlists.CountAsync(w => w.UserId == user.Id);
		return Json(count); // JSON formatında qaytarır
	}

}
