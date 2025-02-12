using Bookle.BL.Services.Interfaces;
using Bookle.DAL.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
		public async Task<IActionResult> Details() 
		{
			return View(await _context.Books.ToListAsync());			
		}

	}
}
