using Bookle.BL.Services.Implements;
using Bookle.BL.Services.Interfaces;
using Bookle.BL.ViewModels.HomeVM;
using Bookle.Core.Entities;
using Bookle.Core.Repositories;
using Bookle.DAL.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bookle.MVC.Controllers
{
	public class AuthorController : Controller
	{
        private readonly BookleDbContext _context;
        private readonly IRatingService _ratingService;
        private readonly ICommentService _commentService;
        private readonly IAuthorService _authorService;
        private readonly UserManager<User> _userManager;
        private readonly IBookService _bookService;





        public AuthorController(BookleDbContext context, IBookService bookService, IRatingService ratingService, ICommentService commentService, IAuthorService authorService, UserManager<User> userManager)
        {
            _context = context;
            _ratingService = ratingService;
            _commentService = commentService;
            _authorService = authorService;
            _userManager = userManager;
            _bookService = bookService;

        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            List<int> wishlistBookIds = new List<int>();

            if (user != null)
            {
                wishlistBookIds = await _context.Wishlists
                    .Where(w => w.UserId == user.Id)
                    .Select(w => w.BookId)
                    .ToListAsync();
            }

            var books = await _bookService.GetAllBooksWithDetailsAsync();
            var authorsWithBookCounts = await _authorService.GetAuthorsWithBookCounts();
            var comments = _context.Comments
        .Include(c => c.User) 
        .Include(c => c.Book) 
        .ToList();
            foreach (var book in books)
            {
                book.IsInWishlist = wishlistBookIds.Contains(book.Id);
            }

            var authors = await _authorService.GetAllFeaturedAuthorProfilesAsync();

            var topRatedBooks = await _bookService.GetTopRatedBooksAsync(6);


            var model = new BooksAndAuthorsVM
            {
                Books = books.ToList(),
                Authors = authors.ToList(),
                TopRatedBooks = topRatedBooks.ToList(),
                Comments = comments.ToList(),
                AuthorsWithBookCounts = authorsWithBookCounts
            };


            return View(model);
        }

        public async Task<IActionResult> Profile(int? id) 
		{
			if (!id.HasValue) return BadRequest();
            var author = await _authorService.GetAuthorWithBooksAsync(id.Value);
            if (author == null) return NotFound();

            return View(author);
		}

	}
}
