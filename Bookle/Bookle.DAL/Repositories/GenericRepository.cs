using Bookle.Core.Entities.Common;
using Bookle.Core.Repositories;
using Bookle.DAL.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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
		var data = await Table.FindAsync(id);
		if (data != null)
		{
			Table.Remove(data);
		}
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
		var data = await Table.FindAsync(id);
		if (data == null) throw new Exception("Data not found");
		data.IsDeleted = false;

	}

	public async Task<int> SaveAsync()
	{
		return await _context.SaveChangesAsync();
	}

	public IEnumerable<T> Search(Expression<Func<T, bool>> predicate)
	{
		return Table.Where(predicate).ToList();
	}

	public async Task SoftDeleteAsync(int id)
	{
	var data =await	Table.FindAsync(id);
		if (data == null) throw new Exception("Data not found");
		data.IsDeleted = true;
	}
}
