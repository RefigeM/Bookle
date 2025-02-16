using Bookle.BL.Exceptions;
using Bookle.BL.Extentions;
using Bookle.BL.Services.Interfaces;
using Bookle.BL.ViewModels.AuthorVMs;
using Bookle.Core.Entities;
using Bookle.Core.Repositories;
using Bookle.DAL.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
		// Müəllifi tapırıq
		var author = await _repo.GetByIdAsync(id);
		if (author == null) throw new NotFoundException();

		// Əlaqəli kitabları tapırıq
		var books = await _context.Books.Where(b => b.AuthorId == id).ToListAsync();

		foreach (var book in books)
		{
			// Kitaba aid olan qiymətləndirmələri tapırıq və silirik
			var ratings = await _context.BookRatings.Where(br => br.BookId == book.Id).ToListAsync();
			_context.BookRatings.RemoveRange(ratings);
		}

		// Kitabları silirik
		_context.Books.RemoveRange(books);

		// Müəllifi silirik
		_context.Authors.Remove(author);

		// Dəyişiklikləri saxlayırıq
		await _repo.SaveAsync();
	}


	public async Task<IEnumerable<Author>> GetAllAuthorsAsync()
	{
		return await _repo.GetAllAsync();
	}

	public async Task<Author> GetAuthorById(int id)
	{
		var author = await _repo.GetByIdAsync(id);
		if (author == null) throw new NotFoundException();
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
