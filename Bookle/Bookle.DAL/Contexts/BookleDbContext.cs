﻿using Bookle.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bookle.DAL.Contexts;

public class BookleDbContext : IdentityDbContext<User>
{
	public BookleDbContext(DbContextOptions options) : base(options)
	{
	}
	public DbSet<Book> Books { get; set; }
	public DbSet<Author> Authors { get; set; }
	public DbSet<BookRating> BookRatings { get; set; }
	public DbSet<Comment> Comments { get; set; }
	public DbSet<Wishlist> Wishlists { get; set; }
    public DbSet<ReadList> ReadLists { get; set; }
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<ContactMessage> ContactMessages { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(typeof(BookleDbContext).Assembly);
		base.OnModelCreating(modelBuilder);
	}
}
