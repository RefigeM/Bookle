using Bookle.DAL.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bookle.MVC.Areas.Admin.Controllers
{
	[Area("Admin")]

	public class BookController : Controller
	{
		private readonly BookleDbContext _context;

		public BookController(BookleDbContext context)
		{
			_context = context;	
		}

		public async  Task<IActionResult> Index()
		{			
			return View(await _context.Books.Include(b => b.Images).ToListAsync());
		}
	}
}
