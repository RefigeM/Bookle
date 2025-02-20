using Bookle.BL.Extentions;
using Bookle.BL.Helpers;
using Bookle.BL.Services.Interfaces;
using Bookle.BL.ViewModels.AuthorVMs;
using Bookle.Core.Entities;
using Bookle.DAL.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bookle.MVC.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = RoleConstants.Author)]

	public class AuthorController(BookleDbContext _context, IWebHostEnvironment _env, IAuthorService _service) : Controller
	{

		public async Task<IActionResult> Index()
		{
			return View(await _service.GetAllAuthorsAsync());
		}
		public async Task<IActionResult> Hide(int? id)
		{
			if (id == null) return BadRequest();
			await _service.SoftDeleteAuthorAsync(id.Value);

			return RedirectToAction(nameof(Index));
		}
		public async Task<IActionResult> Show(int? id)
		{
			if (id == null) return BadRequest();
			await _service.RestoreAuthorAsync(id.Value);

			return RedirectToAction(nameof(Index));
		}

		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null) return BadRequest();

			var author = await _service.GetAuthorById(id.Value);
			if (author == null) return NotFound();

			await _service.DeleteAuthorAsync(id.Value);

			return RedirectToAction(nameof(Index));
		}
		public async Task<IActionResult> Info(int? id)
		{
			if (id == null) return BadRequest();

			var data = await _service.GetAuthorById(id.Value);
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
			await _service.AddAuthorAsync(author);
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
			await _service.UpdateAuthorAsync(id.Value, vm);

			return RedirectToAction(nameof(Index));

		}
		public async Task<IActionResult> ToggleIsFeatured(int? id)
		{
			if (id == null) return BadRequest();
			await _service.ToggleAuthorIsFeaturedAsync(id.Value);
			return RedirectToAction(nameof(Index));

		}

	}
}
