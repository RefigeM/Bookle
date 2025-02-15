using Bookle.BL.Services.Interfaces;
using Bookle.DAL.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Claims;

namespace Bookle.MVC.Controllers
{
	public class HomeController : Controller
	{
		private readonly BookleDbContext _context;
		private readonly IBookService _service;

		public HomeController(BookleDbContext context, IBookService service)
		{
			_context = context;	
			_service= service;	
		}

		public async Task<IActionResult> Index()
		{
			return View(await _service.GetAllBooksAsync());
		}
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null) return BadRequest();

			var data = await _context.Books
				.Include(x => x.BookRatings)
				.Where(x => x.Id == id.Value && !x.IsDeleted)
				.FirstOrDefaultAsync();

			if (data is null) return NotFound();

			string? userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

			if (userId is not null)
			{
				var rating = await _context.BookRatings
					.Where(x => x.UserId == userId && x.BookId == id)
					.Select(x => x.RatingRate)
					.FirstOrDefaultAsync();

				ViewBag.Rating = rating == 0 ? 5 : rating;
			}
			else
			{
				ViewBag.Rating = 5;
			}

			return View(await _service.GetBookByIdAsync(id.Value));
		}

		public async Task<IActionResult> AccessDenied() 
		{			
			return View();
		}
		[Authorize]
		public async Task<IActionResult> Rate(int? bookId, int rate = 1)
		{
			if (!bookId.HasValue) return BadRequest();
			string UserId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)!.Value;
			if (!await _context.Books.AnyAsync(p => p.Id == bookId)) return NotFound();
			var rating = await _context.BookRatings.Where(x => x.BookId == bookId && x.UserId == UserId).FirstOrDefaultAsync();
			if (rating is null)
			{
				await _context.BookRatings.AddAsync(new Core.Entities.BookRating
				{
					BookId = bookId.Value,
					RatingRate = rate,
					UserId = UserId
				});
			}
			else
			{
				rating.RatingRate = rate;
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Details), new { id = bookId });
		}

	}
}
