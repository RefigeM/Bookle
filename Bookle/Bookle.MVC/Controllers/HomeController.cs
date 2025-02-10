using Bookle.DAL.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bookle.MVC.Controllers
{
	public class HomeController : Controller
	{
		private readonly BookleDbContext _context;

		public HomeController(BookleDbContext context)
		{
			_context = context;	
		}

		public async Task<IActionResult> Index()
		{		
			return View(await _context.Books.ToListAsync());
		}
		public async Task<IActionResult> Details() 
		{
			return View(await _context.Books.ToListAsync());			
		}

	}
}
