using Bookle.BL.Extentions;
using Bookle.BL.ViewModels.AuthorVMs;
using Bookle.Core.Entities;
using Bookle.DAL.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bookle.MVC.Areas.Admin.Controllers
{
	[Area("Admin")]

	public class AuthorController(BookleDbContext _context, IWebHostEnvironment _env) : Controller
	{



		public async Task<IActionResult> Index()
		{
			return View(await _context.Authors.ToListAsync());
		}
		public async Task<IActionResult> Hide(int? id)
		{
			if (id == null) return BadRequest();
			var data = await _context.Authors.FindAsync(id);
			if (data == null) return NotFound();
			data.IsDeleted = true;
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}
		public async Task<IActionResult> Show(int? id)
		{
			if (id == null) return BadRequest();
			var data = await _context.Authors.FindAsync(id);
			if (data == null) return NotFound();
			data.IsDeleted = false;
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null) return BadRequest();
			var data = await _context.Authors.FindAsync(id);
			if (data == null) return NotFound();
			_context.Authors.Remove(data);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));

		}

		public ActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Create(AuthorCreateVM vm)
		{
			if (vm.File == null)
			{
				ModelState.AddModelError("File", "File is required");
			}
			else
			{
				if (!vm.File.IsValidType("image"))
					ModelState.AddModelError("File", "File must be an image");

				if (!vm.File.IsValidSize(400))
					ModelState.AddModelError("File", "File must be less than 400kb");
			}

			var imagePath = Path.Combine(_env.WebRootPath, "imgs", "authors");
			Author author = new Author
			{
				AuthorName = vm.AuthorName,
				AuthorImage = imagePath
			};
			if (vm.File != null)
			{
				author.AuthorImage = await vm.File.UploadAsync(imagePath);
			}

			await _context.Authors.AddAsync(author);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}
	}
}


