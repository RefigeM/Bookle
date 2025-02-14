using Bookle.Core.Entities;
using Bookle.Core.Enums;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Bookle.BL.Extentions;

public static class SeedExtention
{
	public static void UseUserSeed(this IApplicationBuilder app)
	{
		using (var scope = app.ApplicationServices.CreateScope())
		{
			var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
			var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
			CreateRoles(roleManager).Wait();
			CreateUsers(userManager).Wait();
		}
	}
	private static async Task CreateRoles(RoleManager<IdentityRole> _roleManager)
	{
		if (!await _roleManager.Roles.AnyAsync())
		{
			foreach (Role item in Enum.GetValues(typeof(Role)))
			{
				await _roleManager.CreateAsync(new IdentityRole(item.GetRole()));
			}
		}
	}
	private static async Task CreateUsers(UserManager<User> _userManager)
	{
		if (!await _userManager.Users.AnyAsync(u => u.NormalizedUserName == "ADMIN"))
		{
			User user = new User();
			user.UserName = "admin";
			user.Email = "admin@gmail.com";
			user.Fullname = "admin";
			string role = nameof(Role.Admin);
			await _userManager.CreateAsync(user, "123");
			await _userManager.AddToRoleAsync(user, role);
		}
	}
}
