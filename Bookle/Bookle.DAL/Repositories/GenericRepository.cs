using Bookle.Core.Entities.Common;
using Bookle.Core.Repositories;
using Bookle.DAL.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Bookle.DAL.Repositories;

public class GenericRepository<T>(BookleDbContext _context) : IGenericRepository<T> where T : BaseEntity, new()
{
	public DbSet<T> Table = _context.Set<T>();
	public async Task AddAsync(T entity)
	{
		await Table.AddAsync(entity);
	}

	public async Task DeleteAsync(int id)
	{
		await Table.Where(x => x.Id == id).ExecuteDeleteAsync();
	}

	public async Task<List<T>> GetAllAsync()
	{
		return await Table.ToListAsync();
	}

	public async Task<T?> GetByIdAsync(int id)
	{
		return await Table.FindAsync(id);
	}

	public IQueryable<T> GetWhere(Func<T, bool> expression)
	{
		return Table.Where(expression).AsQueryable();
	}



	public async Task<bool> IsExistAsync(int id)
	{
		return await Table.AnyAsync(t => t.Id == id);
	}

	public async Task RestoreAsync(int id)
	{
		var author = await _context.Authors.FindAsync(id);
		if (author == null) throw new Exception("Author not found");
		author.IsDeleted = false;

	}

	public async Task<int> SaveAsync()
	{
		return await _context.SaveChangesAsync();
	}

	public async Task SoftDeleteAsync(int id)
	{
		var author = await _context.Authors.FindAsync(id);
		if (author == null) throw new Exception("Author not found");
		author.IsDeleted = true;
	}
}
