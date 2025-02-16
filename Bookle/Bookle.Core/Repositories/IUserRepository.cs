using Bookle.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace Bookle.Core.Repositories;

public interface IUserRepository 
{
	Task<User?> GetByIdAsync(string userId);
	Task<List<User>> GetAllAsync();
	Task AddAsync(User user);
	void Update(User user);
	void Delete(User user);
	Task<int> SaveAsync();
}
