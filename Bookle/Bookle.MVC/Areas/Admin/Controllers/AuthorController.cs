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
		public async Task<IActionResult> Info(int? id)
		{
			if (id == null) return BadRequest();
			var data = await _context.Authors.FindAsync(id);
			if (data == null) return NotFound();

			return View(data);
		}

		public ActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Create(AuthorCreateVM vm)
		{
			if (vm.File != null)
			{
				if (!vm.File.IsValidType("image"))
					ModelState.AddModelError("File", "File must be an image");

				if (!vm.File.IsValidSize(400)) 
					ModelState.AddModelError("File", "File must be less than 400KB");
			}
			else
			{
				ModelState.AddModelError("File", "File is required");
			}

			if (!ModelState.IsValid)
			{
				return View(vm);
			}
			

			var imagePath = Path.Combine(_env.WebRootPath, "imgs", "authors");

			string newFileName = await vm.File.UploadAsync(imagePath);

			Author author = new Author
			{
				AuthorName = vm.AuthorName,
				AuthorImage = "/imgs/authors/" + newFileName  
			};

			await _context.Authors.AddAsync(author);
			await _context.SaveChangesAsync();

			return RedirectToAction(nameof(Index));
		}
		public async Task<ActionResult> Update(int? id)
		{
			if (id == null) return BadRequest();
			var data = await _context.Authors.Where(x => x.Id == id)
				.Select(x => new AuthorUpdateVM
				{
					AuthorName = x.AuthorName,
					FileUrl = x.AuthorImage
				}).FirstOrDefaultAsync();

			if (data == null) return NotFound();
			return View(data);
		}

		[HttpPost]
		public async Task<IActionResult> Update(int? id, AuthorUpdateVM vm)
		{
			if (id == null) return BadRequest();
			var data = await _context.Authors.Where(x => x.Id == id).FirstOrDefaultAsync();
			if (data != null)
			{
				data.AuthorName = vm.AuthorName;

				// Şəkil yüklənirsə, onu serverə yükləyirik
				if (vm.File != null)
				{
					// Faylı serverə yükləyirik
					string newFileName = await vm.File.UploadAsync("wwwroot/imgs/authors");

					// Yüklənən şəkilin URL-ni bazaya yazırıq
					data.AuthorImage = "/imgs/authors/" + newFileName;
				}
			}
			await _context.SaveChangesAsync();

			return RedirectToAction(nameof(Index));

		}

	}
}
