﻿using Bookle.Core.Entities;
using Bookle.Core.Repositories;
using Bookle.DAL.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Bookle.DAL.Repositories;

public class BookRepository : GenericRepository<Book>, IBookRepository
{
	private readonly BookleDbContext _context;	


	public BookRepository(BookleDbContext context) : base(context)
	{
		_context = context;
	}

	public async Task<IEnumerable<Book>> GetAllWithDetailsAsync()
	{
	return await _context.Books.Include(b => b.Author)
			.Include(b=>b.Images)
			.ToListAsync();	
	}

	public async Task<Book> GetByIdWithDetailsAsync(int id)
	{
		return await _context.Books
	   .Include(b => b.Author)  
	   .Include(b => b.Images)   
	   .FirstOrDefaultAsync(b => b.Id == id);
		
	}
}
