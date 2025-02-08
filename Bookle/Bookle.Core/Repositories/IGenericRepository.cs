using Bookle.Core.Entities.Common;

namespace Bookle.Core.Repositories;

public interface IGenericRepository<T> where T : BaseEntity, new()	
{
	IQueryable<T> GetAll();
	Task<T?> GetByIdAsync(int id);
	IQueryable<T> GetWhere(Func<T, bool> expression);
	Task<bool> IsExistAsync(int id);
	Task AddAsync(T entity);
	Task DeleteAsync(int id);
	Task<int> SaveAsync();
}
