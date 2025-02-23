using Bookle.BL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Bookle.MVC.Areas.Admin.Controllers
{
	public class SearchController(IBookService _service) : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
		public async Task<IActionResult> BookSearch(string searchQuery)
		{
			// Axtarış boşdursa, kitablar siyahısına yönləndiririk
			if (string.IsNullOrEmpty(searchQuery))
			{
				return RedirectToAction("Index", "Book"); // Kitabların ümumi siyahısına yönləndiririk
			}

			// Axtarış nəticələrini alırıq
			var books = await _service.SearchBooksAsync(searchQuery);

			// Nəticələri View-a göndəririk
			ViewData["searchQuery"] = searchQuery;
			return View("Index", books); // Burada 'Index' görünüşü istifadə edilir
		}
	}
}
