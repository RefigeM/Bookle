using Bookle.BL.Extentions;
using Bookle.BL.ViewModels.AuthsVMs;
using Bookle.Core.Entities;
using Bookle.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bookle.MVC.Controllers
{
	public class AccountController(UserManager<User> _userManager, SignInManager<User> _signManager, RoleManager<IdentityRole> _roleManager) : Controller
	{
		private bool IsAuthenticated => HttpContext.User.Identity?.IsAuthenticated ?? false;
		public IActionResult Register()
		{
			if (IsAuthenticated) return RedirectToAction("Index", "Home");
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Register(RegisterVM vm)
		{
			if (!ModelState.IsValid)
				return View();

			User user = new User
			{
				Fullname = vm.Fullname,
				Email = vm.Email,
				UserName = vm.Username
			};
			var result = await _userManager.CreateAsync(user, vm.Password);
			if (!result.Succeeded)
			{
				foreach (var err in result.Errors)
				{
					ModelState.AddModelError("", err.Description);
				}
				return View(vm);
			}
			var roleResult = await _userManager.AddToRoleAsync(user, nameof(Role.User));

			if (!roleResult.Succeeded)
			{
				foreach (var err in roleResult.Errors)
				{
					ModelState.AddModelError("", err.Description);
				}
				return View(vm);
			}
			return RedirectToAction("Login", "Account");
		}
		public IActionResult Login()
		{
			if (IsAuthenticated) return RedirectToAction("Index", "Home");
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Login(LoginVM vm)
		{
			if (!ModelState.IsValid) return View();
			User user = null;
			if (vm.UsernameOrEmail.Contains("@"))
				user = await _userManager.FindByEmailAsync(vm.UsernameOrEmail);
			else
				user = await _userManager.FindByNameAsync(vm.UsernameOrEmail);
			if (user is null)
			{
				ModelState.AddModelError("", "username or password wrong!");
				return View();
			}
			var result = await _signManager.PasswordSignInAsync(user, vm.Password, vm.RememberMe, true);
			if (!result.Succeeded)
			{
				if (result.IsLockedOut)
				{
					ModelState.AddModelError("", "wait until" + user.LockoutEnd!.Value.ToString("yyyy-MM-dd HH:mm:ss"));

				}
				if (result.IsNotAllowed)
				{
					ModelState.AddModelError("", "Username or password is wrong!");

				}
				return View();

			}
			return RedirectToAction("Index", "Home");

		}
		public async Task<IActionResult> CreateRoles()
		{
			foreach (Role item in Enum.GetValues(typeof(Role)))
			{
				await _roleManager.CreateAsync(new IdentityRole(item.GetRole()));
			}
			return Ok();
		}
		[Authorize]
		public async Task<IActionResult> Logout()
		{
			await _signManager.SignOutAsync();
			return RedirectToAction(nameof(Login));
		}

	}
}
