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
        public async Task<IActionResult> Index()
        {
            return View(await _userService.GetAllUsersAsync());
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


	}
}
