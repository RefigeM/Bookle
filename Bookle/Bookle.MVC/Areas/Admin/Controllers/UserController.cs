using Bookle.BL.Services.Interfaces;
using Bookle.DAL.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bookle.MVC.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class UserController(IUserService _userService, IWebHostEnvironment _env, IAuthorService _service) : Controller
    {
		public async Task<IActionResult> Index(int? page = 1, int? take = 10)
		{
			if (!page.HasValue) page = 1;
			if (!take.HasValue) take = 10;

			var query = _userService.GetAllUsers();

			decimal userCount = await query.CountAsync();

			var data = await query
				.Skip(take.Value * (page.Value - 1))
				.Take(take.Value)
				.ToListAsync();

			decimal pageCount = Math.Ceiling(userCount / (decimal)take.Value);
			ViewBag.PageCount = pageCount;
			ViewBag.Take = take;
			ViewBag.AktivePage = page;

			return View(data);
		}



		[HttpGet("Delete/{userId}")]
		public async Task<IActionResult> Delete([FromRoute] string userId)
		{
			if (string.IsNullOrEmpty(userId)) return BadRequest("Invalid User ID");

			var user = await _userService.GetUserByIdAsync(userId);
			if (user == null) return NotFound("User not found");

			await _userService.DeleteUserAsync(userId);
			return RedirectToAction(nameof(Index));
		}
		public async Task<IActionResult> UserSearch(string searchQuery)
		{
			if (string.IsNullOrEmpty(searchQuery))
			{
				return RedirectToAction("Index");  
			}

			var users = await _userService.SearchUsersAsync(searchQuery);
			ViewData["searchQuery"] = searchQuery; 
			return View("Index", users);
		}


	}
}
