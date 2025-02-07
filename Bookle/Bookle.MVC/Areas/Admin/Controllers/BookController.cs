using Bookle.BL.Extentions;
using Bookle.BL.ViewModels.BookVMs;
using Bookle.Core.Entities;
using Bookle.Core.Enums;
using Bookle.DAL.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Bookle.MVC.Areas.Admin.Controllers
{
	[Area("Admin")]

	public class BookController(BookleDbContext _context, IWebHostEnvironment _env) : Controller
	{
		

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
		public async Task<IActionResult> Create() 
		{
			ViewBag.Authors = await _context.Authors.Where(x => !x.IsDeleted).ToListAsync();
			ViewBag.Languages = new SelectList(new List<string> { "English", "Azerbaijani", "Turkish", "French", "Spanish" });
			ViewBag.Formats = new SelectList(Enum.GetNames(typeof(Format)));
			ViewBag.Genres = new SelectList(Enum.GetNames(typeof(Genre)));

			return View();

		}
		[HttpPost]
		public async Task<IActionResult> Create(BookCreateVM vm)
		{
			if (vm.File != null) 
			{
				if (!vm.File.IsValidType("image"))
					ModelState.AddModelError("File", "File must be an image");
				if (!vm.File.IsValidSize(400))
					ModelState.AddModelError("File", "File must be less than 400");
			}
			if (vm.OtherFiles != null && vm.OtherFiles.Any())
			{
				if (!vm.OtherFiles.All(x => x.IsValidType("image")))
				{
					string fileNames = string.Join(',', vm.OtherFiles.Where(x => !x.IsValidType("image")).Select(x => x.FileName));
					ModelState.AddModelError("OtherFiles", fileNames + "is/are not an image");
				}
				if (!vm.OtherFiles.All(x => x.IsValidSize(400)))
				{
					string fileNames = string.Join(',', vm.OtherFiles.Where(x => !x.IsValidSize(400)).Select(x => x.FileName));
					ModelState.AddModelError("OtherFiles", fileNames + "is/are bigger than 400 kb.");
				}
			}
			



			if (!ModelState.IsValid)
			{
				ViewBag.Authors = await _context.Authors.Where(x => !x.IsDeleted).ToListAsync();
				ViewBag.Languages = new SelectList(new List<string> { "English", "Azerbaijani", "Turkish", "French", "Spanish" });
				ViewBag.Formats = new SelectList(Enum.GetNames(typeof(Format)));
				ViewBag.Genres = new SelectList(Enum.GetNames(typeof(Genre)));

				return View(vm);
			}
			if (!await _context.Authors.AnyAsync(x => x.Id == vm.AuthorId)) 
			{
				ViewBag.Author = await _context.Authors.Where(x => !x.IsDeleted).ToListAsync();
				ModelState.AddModelError("AuthorId", "Author not found");
				return View(vm);	
			}
			Book book = vm;
			book.CoverImageUrl = await vm.File!.UploadAsync(_env.WebRootPath, "imgs", "books");
			if (vm.OtherFiles != null && vm.OtherFiles.Any())
			{
				book.Images = vm.OtherFiles.Select(x => new BookImage
				{
					Book = book,
					ImageUrl = x.UploadAsync(_env.WebRootPath, "imgs", "books").Result
				})
				.ToList();
			}
			else
			{
				book.Images = new List<BookImage>();  
			}
			await _context.Books.AddAsync(book);
			await _context.SaveChangesAsync();	
			return RedirectToAction(nameof(Index));	
		}
	}
}
