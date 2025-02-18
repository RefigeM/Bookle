using Bookle.BL.Exceptions;
using Bookle.BL.Extentions;
using Bookle.BL.Services.Interfaces;
using Bookle.BL.ViewModels.AuthorVMs;
using Bookle.Core.Entities;
using Bookle.Core.Repositories;
using Bookle.DAL.Contexts;
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
					BookCount = a.Books.Count()
				}).ToListAsync();
	}

	public async Task<IEnumerable<Author>> GetAllAuthorsAsync()
	{
		return await _repo.GetAllAsync();
	}

	public async Task<List<Author>> GetAllAuthorsWithBooksAsync()
	{
		var authors = await _repo.GetAllAuthorsWithDetailsAsync();
		if (authors.Count == 0) throw new NotFoundException();
		return authors;
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
					AuthorImg = a.AuthorImage
				}).FirstOrDefaultAsync(a => a.AuthorId == id);
		if (author is null) throw new NotFoundException();

		return author;

	}

	public Task<List<BookCountOfAuthor>> GetBookCountOfAuthor(int? id)
	{
		throw new NotImplementedException();
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

	public async Task UpdateAuthorAsync(int id, AuthorUpdateVM vm)
	{
		var author = await _repo.GetByIdAsync(id);
		if (author == null) throw new NotFoundException();

		author.AuthorName = vm.AuthorName;
		if (vm.File != null)
		{
			string newFileName = await vm.File.UploadAsync("wwwroot/imgs/authors");
			author.AuthorImage = "/imgs/authors/" + newFileName;
		}
		await _repo.SaveAsync();
	}


}
