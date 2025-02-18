using Bookle.BL.Services.Implements;
using Bookle.BL.Services.Interfaces;
using Bookle.BL.ViewModels.HomeVM;
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
		private readonly IRatingService _ratingService;
		private readonly ICommentService _commentService;
		private readonly IAuthorService _authorService;
		

		public HomeController(BookleDbContext context, IBookService service, IRatingService ratingService, ICommentService commentService,IAuthorService authorService)
		{
			_context = context;	
			_service= service;	
			_ratingService= ratingService;	
			_commentService= commentService;	
			_authorService= authorService;	
		}

		public async Task<IActionResult> Index()
		{
			var books = await _service.GetAllBooksWithDetailsAsync();
			var authors = await _authorService.GetAllAuthorProfilesAsync();
			var model = new BooksAndAuthorsVM
			{
				Books = books.ToList(),
				Authors = authors.ToList()

			};
			return View(model);
		}
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null) return BadRequest();

			var book = await _context.Books
				.Include(x => x.BookRatings)
				 .Include(x => x.Comments)
				 .ThenInclude(x => x.User)
				.Where(x => x.Id == id.Value && !x.IsDeleted)
				.FirstOrDefaultAsync();

			if (book is null) return NotFound();

			string? userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

			if (userId is not null)
			{
				var rating = await _context.BookRatings
					.Where(x => x.UserId == userId && x.BookId == id)
					.Select(x => x.RatingRate)
					.FirstOrDefaultAsync();

				ViewBag.Rating = rating > 0 ? rating : 0;
				ViewBag.UserRating = rating;
			}
			else
			{
				ViewBag.Rating = 0; 
			}

			return View(book);
		}


		public async Task<IActionResult> AccessDenied() 
		{			
			return View();
		}
	
		[HttpPost]
		public IActionResult SubmitRating(int bookId, int star)
		{
			string userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

			if (userId != null)
			{
				_ratingService.AddRating(bookId, userId, star);
			}

			return RedirectToAction("Details", new { id = bookId });
		}
		[HttpPost]
		public IActionResult AddComment(int bookId, string content)
		{
			string userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "";

			if (string.IsNullOrWhiteSpace(userId))
				return Unauthorized();

			_commentService.AddComment(bookId, userId, content);

			return RedirectToAction("Details", "Home", new { id = bookId });
		}
	}
}
