using Bookle.BL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Bookle.MVC.Controllers
{
	public class AuthorController : Controller
	{
		private readonly IAuthorService _authorService;

		public AuthorController(IAuthorService authorService)
		{
			_authorService = authorService;	
		}

		public async Task<IActionResult> Index()
		{
			return View( await _authorService.GetAllAuthorProfilesAsync());
		}
		public async Task<IActionResult> Profile(int? id) 
		{
			if (!id.HasValue) return BadRequest();
			return View(await _authorService.GetAuthorDetailsWithIdAsync(id.Value));
		}

	}
}
