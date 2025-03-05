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

            var lastFiveBooks = books.OrderByDescending(b => b.Id).Take(5).ToList();
            var lastFiveAuthors = authors.OrderByDescending(a => a.Id).Take(5).ToList();
            var lastFiveBlogs = blogs.OrderByDescending(b => b.Id).Take(5).ToList();
            var lastFiveTopRatedBooks = topRatedBooks.OrderByDescending(b => b.Id).Take(5).ToList();
            var lastFiveUsers = users.OrderByDescending(b => b.Id).Take(5).ToList();

			var totalBookCount = books.Count();
			var totalBlogCount = blogs.Count();
			var totalAuthorCount = authors.Count();
			var totalUserCount = users.Count();

			var model = new DashboardVm
			{
				Books = lastFiveBooks,
				Authors = lastFiveAuthors,
				Blogs = lastFiveBlogs,
				TopRatedBooks = lastFiveTopRatedBooks,
				Users= lastFiveUsers,
				TotalBookCount = totalBookCount,
				TotalBlogCount = totalBlogCount,	
				TotalAuthorCount = totalAuthorCount,	
				TotalUserCount = totalUserCount


            };

			return View(model);
		}
	}
}
