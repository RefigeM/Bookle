using Bookle.BL.Services.Interfaces;
using Bookle.BL.ViewModels.HomeVM;
using Bookle.Core.Entities;
using Bookle.DAL.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly UserManager<User> _userManager;



        public HomeController(BookleDbContext context, IBookService service, IRatingService ratingService, ICommentService commentService, IAuthorService authorService, UserManager<User> userManager)
        {
            _context = context;
            _service = service;
            _ratingService = ratingService;
            _commentService = commentService;
            _authorService = authorService;
            _userManager = userManager;

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

            var books = await _service.GetAllBooksWithDetailsAsync();

            foreach (var book in books)
            {
                book.IsInWishlist = wishlistBookIds.Contains(book.Id);
            }

            var authors = await _authorService.GetAllFeaturedAuthorProfilesAsync();

            var topRatedBooks = await _service.GetTopRatedBooksAsync(6);

            var model = new BooksAndAuthorsVM
            {
                Books = books.ToList(),
                Authors = authors.ToList(),
                TopRatedBooks=topRatedBooks.ToList()
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

                // **Kitab oxunmuş siyahısındadırmı?**
                //bool isReaded = await _context.ReadLists
                //    .AnyAsync(r => r.UserId == userId && r.BookId == id && r.IsReaded);

                //ViewBag.IsReaded = isReaded;
            }
            else
            {
                ViewBag.Rating = 0;
                //ViewBag.IsReaded = false;
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

        public async Task<IActionResult> TopBooks()
        {
            var topBooks=  await _service.GetTopRatedBooksAsync(6);
            return View(topBooks);  
        }

       
    }
}
