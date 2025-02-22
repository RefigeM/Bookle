using Bookle.BL.Exceptions;
using Bookle.BL.Services.Interfaces;
using Bookle.BL.ViewModels.UserVMs;
using Bookle.Core.Entities;
using Bookle.Core.Repositories;
using Bookle.DAL.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace Bookle.BL.Services.Implements;

public class UserService : IUserService
{
	private readonly IUserRepository _userRepository;
	private readonly UserManager<User> _userManager;
	private readonly SignInManager<User> _signInManager;
	private readonly BookleDbContext _context;

	public UserService(IUserRepository userRepository, UserManager<User> userManager, SignInManager<User> signInManager,BookleDbContext context)
	{
		_userRepository = userRepository;
		_userManager = userManager;
		_signInManager = signInManager;
		_context = context;
	}

	public async Task<IdentityResult> CreateUserAsync(User user, string password)
	{
		var result = await _userManager.CreateAsync(user, password);
		if (result.Succeeded)
		{
			await _userRepository.SaveAsync();
		}
		return result;
	}

	public async Task<bool> AuthenticateUserAsync(string email, string password)
	{
		var user = await _userManager.FindByEmailAsync(email);
		if (user == null) return false;

		var result = await _signInManager.PasswordSignInAsync(user, password, false, false);
		return result.Succeeded;
	}

	public async Task DeleteUserAsync(string userId)
	{
		var user = await _userRepository.GetByIdAsync(userId);
		if (user == null) throw new NotFoundException("User with the specified ID not found");
		var ratings = _context.BookRatings.Where(r => r.UserId == userId);
		_context.BookRatings.RemoveRange(ratings);

		var comments = _context.Comments.Where(c => c.UserId == userId);
		_context.Comments.RemoveRange(comments);
		_userRepository.Delete(user);
		await _userRepository.SaveAsync();
	}

	public async Task<List<User>> GetAllUsersAsync()
	{
		List<User> users = await _userRepository.GetAllAsync();
		if (users.Count == 0) throw new NotFoundException("User with the specified ID not found");
		return users;

	}

	public async Task<User> GetUserByIdAsync(string userId)
	{
		var user = await _userRepository.GetByIdAsync(userId);
		if (user is null) throw new NotFoundException("User with the specified ID not found");
		return user;
	}



	public async Task SoftDeleteUserAsync(string userId)
	{
		var user = await _userRepository.GetByIdAsync(userId);
		if (user == null) throw new NotFoundException("User with the specified ID not found");

		user.IsDeleted = true;
		_userRepository.Update(user);
		await _userRepository.SaveAsync();
	}

	public async Task UpdateUserAsync(string userId, UserUpdateVM vm)
	{
		var user = await _userRepository.GetByIdAsync(userId);
		if (user == null) throw new NotFoundException("User with the specified ID not found");
		_userRepository.Update(user);
		await _userRepository.SaveAsync();
	}

	public async Task RestoreUserAsync(string userId)
	{
		var user = await _userRepository.GetByIdAsync(userId);
		if (user is null) throw new NotFoundException("User with the specified ID not found");

		user.IsDeleted = false;
		_userRepository.Update(user);
		await _userRepository.SaveAsync();

	}

    public async Task<UserProfileVM> GetUserProfileAsync(string userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);

        if (user == null)
        {
            return null; // İstifadəçi tapılmadıqda null qaytarılır
        }

        // User modelini ViewModel formatına çeviririk
        var userProfile = new UserProfileVM
        {
            UserId = user.Id,
            FullName = user.Fullname,
            Email = user.Email,
            Address = user.Address,
            ProfileImageUrl = user.ProfilImage
        };

        return userProfile;
    }
}

