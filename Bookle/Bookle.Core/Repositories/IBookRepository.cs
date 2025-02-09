﻿using Bookle.Core.Entities;

namespace Bookle.Core.Repositories
{
	public interface IBookRepository : IGenericRepository<Book>
	{
		Task<Book> GetByIdWithDetailsAsync(int id);
	}
}
