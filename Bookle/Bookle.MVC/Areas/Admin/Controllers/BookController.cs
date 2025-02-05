using Bookle.BL.ViewModels.BookVMs;
using Bookle.Core.Entities;
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

		public async Task<IActionResult> Index()
		{
			return View(await _context.Books.Include(b => b.Images).Include(b => b.Author).ToListAsync());
		}
		public async Task<IActionResult> Delete(int id)
		{
			var data = await _context.Books.Include(b => b.Author).FirstOrDefaultAsync(b => b.Id == id);
			if (data == null) return NotFound();
			_context.Books.Remove(data);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}
		public async Task<IActionResult> Hide(int id)
		{
			var data = await _context.Books.Include(b => b.Author).FirstOrDefaultAsync(b => b.Id == id);
			if (data == null) return NotFound();
			if (data != null)
			{
				data.IsDeleted = true;
				await _context.SaveChangesAsync();
			}
			
			return RedirectToAction(nameof(Index));
		}
		public async Task<IActionResult> Show(int id) 
		{
			var data = await _context.Books.Include(b => b.Author).FirstOrDefaultAsync(b => b.Id == id);
			if (data == null) return NotFound();
			data.IsDeleted = false;
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));

		}
		public IActionResult Create() 
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Create(BookCreateVM vm)
		{
			if(!ModelState.IsValid) return View(vm);
			return View();	
		}
	}
}
