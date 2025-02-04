using Bookle.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bookle.DAL.Contexts;

public class BookleDbContext : DbContext
{
	public BookleDbContext(DbContextOptions options) : base(options)
	{
	}
	public DbSet<Book> Books { get; set; }
	public DbSet<BookImage> BookImages { get; set; }
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(typeof(BookleDbContext).Assembly);
		base.OnModelCreating(modelBuilder);
	}
}
