using Bookle.BL.Exceptions;
using Bookle.BL.Extentions;
using Bookle.BL.Services.Interfaces;
using Bookle.BL.ViewModels.AuthorVMs;
using Bookle.Core.Entities;
using Bookle.Core.Repositories;
using Bookle.DAL.Contexts;
using Bookle.DAL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Bookle.BL.Services.Implements;

public class AuthorService(IAuthorRepository _repo, BookleDbContext _context) : IAuthorService
{
	public async Task AddAuthorAsync(Author author)
	{
		if (author == null) throw new NotFoundException("author is null");

		await _repo.AddAsync(author);
		await _repo.SaveAsync();
	}

	//public async Task DeleteAuthorAsync(int id)
	//{
	//	var author = await _repo.GetByIdAsync(id);
	//	if (author == null) throw new NotFoundException();
	//	await _repo.DeleteAsync(id);
	//	await _repo.SaveAsync();
	//}
	public async Task DeleteAuthorAsync(int id)
	{
		var author = await _repo.GetByIdAsync(id);
		if (author == null) throw new NotFoundException();

		var books = await _context.Books.Where(b => b.AuthorId == id).ToListAsync();
		foreach (var book in books)
		{
			var ratings = await _context.BookRatings.Where(br => br.BookId == book.Id).ToListAsync();
			_context.BookRatings.RemoveRange(ratings);
		}

		_context.Books.RemoveRange(books);

		_context.Authors.Remove(author);

		await _repo.SaveAsync();
	}

	public Task<List<AuthorAllDataVM>> GetAllAuthorProfiles()
	{
		throw new NotImplementedException();
	}

	public async Task<List<AuthorAllDataVM>> GetAllAuthorProfilesAsync()
	{
		return await _context.Authors
			.Include(a => a.Books)
			.Select(a => new AuthorAllDataVM
			{
				AuthorId = a.Id,
				AuthorName = a.AuthorName,
				AuthorImg = a.AuthorImage,
				BookCount = a.Books != null ? a.Books.Count() : 0 
			})
			.ToListAsync();
	}

    public async Task<IEnumerable<Author>> GetAllAuthorsAsync()
    {
        var authors = await _repo.GetAllAuthorsWithDetailsAsync();

        if (authors == null || !authors.Any())
        {
            return new List<Author>(); 
        }

        return authors; 
    }



    public async Task<List<Author>> GetAllAuthorsWithBooksAsync()
	{
		var authors = await _repo.GetAllAuthorsWithDetailsAsync();
		if (authors.Count == 0) throw new NotFoundException();
		return authors;
	}

	public async Task<List<AuthorAllDataVM>> GetAllFeaturedAuthorProfilesAsync()
	{
		return await _context.Authors
					.Where(a => a.IsFeatured == true)
					.Include(a => a.Books)
					.Select(a => new AuthorAllDataVM
					{
						AuthorId = a.Id,
						AuthorName = a.AuthorName,
						AuthorImg = a.AuthorImage,
						BookCount = a.Books != null ? a.Books.Count() : 0 
					})
					.ToListAsync();
	}

	public async Task<Author> GetAuthorById(int id)
	{
		var author = await _repo.GetByIdAsync(id);
		if (author == null) throw new NotFoundException();
		return author;
	}

	public async Task<AuthorAllDataVM?> GetAuthorDetailsWithIdAsync(int id)
	{
		var author = await _context.Authors
				.Include(a => a.Books)
				.Select(a => new AuthorAllDataVM
				{
					AuthorId = a.Id,
					AuthorName = a.AuthorName,
					AuthorImg = a.AuthorImage,
					Country=a.Country,
					BirthYear=a.BirthYear,
					DeathYear=a.DeathYear,
					Biography=a.Biography	

					
				}).FirstOrDefaultAsync(a => a.AuthorId == id);
		if (author is null) throw new NotFoundException();

		return author;

	}

    public async Task<List<BookCountOfAuthor>> GetAuthorsWithBookCounts()
    {
        return await _context.Authors
			.Include (a => a.Books)	
			.ThenInclude(b => b.BookRatings)
            .Select(a => new BookCountOfAuthor
            {
                AuthorId = a.Id,
                AuthorFullName = a.AuthorName,
                BookCount = a.Books.Count()
            })
            .ToListAsync();
    }

   



   

    public async Task RestoreAuthorAsync(int id)
	{
		await _repo.RestoreAsync(id);
		await _repo.SaveAsync();
	}

	public async Task SoftDeleteAuthorAsync(int id)
	{
		await _repo.SoftDeleteAsync(id);
		await _repo.SaveAsync();
	}

	public async Task ToggleAuthorIsFeaturedAsync(int id)
	{
		var author = await _repo.GetByIdAsync(id);
		if (author == null) throw new NotFoundException("Book is null");

		author.IsFeatured = !author.IsFeatured;
		await _repo.SaveAsync();
	}

	public async Task UpdateAuthorAsync(int id, AuthorUpdateVM vm)
	{
		var author = await _repo.GetByIdAsync(id);
		if (author == null) throw new NotFoundException();

		author.AuthorName = vm.AuthorName;
		author.Country = vm.Country;	
		author.Biography = vm.Biography;	
		author.BirthYear = vm.BirthYear;	
		author.DeathYear = vm.DeathYear;	
		author.FacebookUrl = vm.FacebookUrl;	
		author.TwitterUrl = vm.TwitterUrl;
        author.InstagramUrl = vm.InstagramUrl;
		author.LinkedInUrl = vm.LinkedInUrl;


        if (vm.File != null)
		{
			string newFileName = await vm.File.UploadAsync("wwwroot/imgs/authors");
			author.AuthorImage = "/imgs/authors/" + newFileName;
		}
		await _repo.SaveAsync();
	}

    public async Task<AuthorAllDataVM?> GetAuthorWithBooksAsync(int authorId)
    {
        var author = await _context.Authors
            .Include(a => a.Books) // Müəllifin kitablarını da gətir
            .FirstOrDefaultAsync(a => a.Id == authorId);

        if (author == null) return null;

        return new AuthorAllDataVM
        {
            AuthorId = author.Id,
            AuthorName = author.AuthorName,
            AuthorImg = author.AuthorImage,
            Biography = author.Biography,
            BookCount = author.Books.Count,
            Country = author.Country,
            FacebookUrl = author.FacebookUrl,
            TwitterUrl = author.TwitterUrl,
            InstagramUrl = author.InstagramUrl,
            LinkedInUrl = author.LinkedInUrl,
            BirthYear = author.BirthYear,
            DeathYear = author.DeathYear,
            Books = author.Books.ToList()
        };
    }

	public async Task<IEnumerable<Author>> SearchAuthorsAsync(string searchQuery)
	{
		return string.IsNullOrEmpty(searchQuery)
					? await _repo.GetAllAuthorsWithDetailsAsync()
					: await _repo.SearchByAuthorAsync(searchQuery);
	}
}
