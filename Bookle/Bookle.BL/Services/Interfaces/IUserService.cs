using Bookle.BL.ViewModels.UserVMs;
using Bookle.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Bookle.BL.Services.Interfaces;

public interface IUserService
{
	Task<List<User>> GetAllUsersAsync();
	Task<User> GetUserByIdAsync(string userId);
	Task<IdentityResult> CreateUserAsync(User user, string password);
	Task DeleteUserAsync(string userId);
	Task RestoreUserAsync(string userId);
	Task SoftDeleteUserAsync(string userId);
	Task UpdateUserAsync(string userId, UserUpdateVM vm);
	Task<bool> AuthenticateUserAsync(string email, string password);
}
