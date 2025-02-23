using Bookle.BL.Services.Interfaces;
using Bookle.BL.ViewModels.DashboardVM;
using Bookle.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Bookle.MVC.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles ="Admin")]

	public class DashboardController(IBookService _bookService,IAuthorService _authorService,IBlogService _blogService, IUserService _userService, UserManager<User> _userManager) : Controller
	{
		public async Task<IActionResult> Index()
		{
			var books = await _bookService.GetAllBooksWithDetailsAsync();
			var authorsWithBookCounts = await _authorService.GetAuthorsWithBookCounts();
			var blogs = await _blogService.GetAllPostsVisiblePostsAsync();
			var authors = await _authorService.GetAllAuthorsWithBooksAsync();
			var topRatedBooks = await _bookService.GetTopRatedBooksAsync(6);

			var users = _userService.GetAllUsers();



			var model = new DashboardVm
			{
				Books = books.ToList(),
				Authors = authors.ToList(),
				Blogs = blogs.ToList(),
				TopRatedBooks = topRatedBooks.ToList(),
				Users= users.ToList()
			};

			return View(model);
		}
	}
}
