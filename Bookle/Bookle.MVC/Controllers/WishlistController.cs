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

		return RedirectToAction("Index");
	}


	public async Task<IActionResult> RemoveFromWishlist(int bookId)
	{
		var user = await _userManager.GetUserAsync(User);
		var wishlistItem = _context.Wishlists.FirstOrDefault(w => w.UserId == user.Id && w.BookId == bookId);

		if (wishlistItem != null)
		{
			_context.Wishlists.Remove(wishlistItem);
			await _context.SaveChangesAsync();
		}

		return RedirectToAction("Index");
	}
}
