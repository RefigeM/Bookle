using Bookle.Core.Entities;
using Bookle.DAL.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bookle.MVC.Controllers
{
	public class ReadListController : Controller
	{
		private readonly BookleDbContext _context;
		private readonly UserManager<User> _userManager;

		public ReadListController(BookleDbContext context, UserManager<User> userManager)
		{
			_context = context;	
			_userManager = userManager;	
		}

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null) return Unauthorized();

            var books = await _context.ReadLists
                .Where(r => r.UserId == userId)
                .Include(r => r.Book)
                .ThenInclude(b => b.Author)
                .Select(r => r.Book)
                .ToListAsync();

            return View(books);
        }


        [HttpPost]
        public async Task<IActionResult> Add(int bookId)
        {
            var userId = _userManager.GetUserId(User); // Aktiv istifadəçini tapırıq
            if (userId == null) return Unauthorized();

            // Kitabın varlığını yoxlayın
            var bookExists = await _context.Books.AnyAsync(b => b.Id == bookId);
            if (!bookExists)
            {
                // Kitab mövcud deyil
                ModelState.AddModelError("", "Seçdiyiniz kitab mövcud deyil.");
                return RedirectToAction("Index", "Books"); // Kitablar səhifəsinə yönləndirir
            }

            var existingRecord = await _context.ReadLists
                .FirstOrDefaultAsync(r => r.UserId == userId && r.BookId == bookId);

            if (existingRecord == null)
            {
                var readListEntry = new ReadList
                {
                    UserId = userId,
                    BookId = bookId
                };

                _context.ReadLists.Add(readListEntry);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Home"); // Kitablar səhifəsinə yönləndirir
        }

        [HttpGet]
        public IActionResult RemoveFromReadList(int bookId)
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null) return Unauthorized();

            var readListEntry = _context.ReadLists
                .FirstOrDefault(r => r.UserId == userId && r.BookId == bookId);

            if (readListEntry != null)
            {
                _context.ReadLists.Remove(readListEntry);
                _context.SaveChanges();
            }

            return RedirectToAction("Index", "ReadList");
        }

    }
}
