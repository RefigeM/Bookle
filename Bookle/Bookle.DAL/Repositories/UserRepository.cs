using Bookle.Core.Entities;
using Bookle.Core.Repositories;
using Bookle.DAL.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Bookle.DAL.Repositories;

public class UserRepository : IUserRepository
{
	private readonly BookleDbContext _context;

	public UserRepository(BookleDbContext context)
	{
		_context = context;
	}

	public async Task AddAsync(User user)
	{
		await _context.Users.AddAsync(user);
		await _context.SaveChangesAsync();
	}

	public void Delete(User user)
	{
		_context.Users.Remove(user);
		_context.SaveChanges();
	}

	public async Task<List<User>> GetAllAsync()
	{
		return await _context.Users.ToListAsync();
	}

    public async Task<User?> GetByIdAsync(string userId)
    {
        var user = await _context.Users
    .Where(u => u.Id.ToLower() == userId.ToLower())
    .FirstOrDefaultAsync();
		if (user == null) { throw new Exception("Not found"); }
		return user;	

    }




    public async Task<int> SaveAsync()
	{
		return await _context.SaveChangesAsync();
	}

	public void Update(User user)
	{
		_context.Users.Update(user);
		_context.SaveChanges();

	}	
}
