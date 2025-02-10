using Bookle.BL.Exceptions;
using Bookle.BL.Extentions;
using Bookle.BL.Services.Interfaces;
using Bookle.BL.ViewModels.BookVMs;
using Bookle.Core.Entities;
using Bookle.Core.Repositories;
using Bookle.DAL.Contexts;

namespace Bookle.BL.Services.Implements;

public class BookService(IBookRepository _repo, BookleDbContext _context) : IBookService
{
	public async Task AddBookAsync(Book book)
	{
		if (book == null) throw new NotFoundException("Book is null");
		await _repo.AddAsync(book);
		await _repo.SaveAsync();
	}

	public async Task DeleteBookAsync(int id)
	{
		var book = await _repo.GetByIdAsync(id);
		if (book == null) throw new NotFoundException("Book is null");
		await _repo.DeleteAsync(id);
		await _repo.SaveAsync();
	}

	public async Task<IEnumerable<Book>> GetAllBooksAsync()
	{
		var data = await _repo.GetAllWithDetailsAsync();
		if (data == null) throw new NotFoundException();
		return data;
	}

	public async Task<Book> GetBookByIdAsync(int id)
	{
		var book = await _repo.GetByIdWithDetailsAsync(id);
		if (book == null) throw new NotFoundException();
		return book;

	}

	public async Task RestoreBookAsync(int id)
	{
		await _repo.RestoreAsync(id);
		await _repo.SaveAsync();
	}

	public async Task SoftDeleteBookAsync(int id)
	{
		await _repo.SoftDeleteAsync(id);
		await _repo.SaveAsync();
	}

	public async Task UpdateBookAsync(int id, BookUpdateVM vm)
	{
		var book = await _repo.GetByIdAsync(id);
		if (book == null) throw new NotFoundException();

		book.Title = vm.Title;
		book.Price = vm.Price;
		book.Description = vm.Description;
		book.ShortDescription = vm.ShortDescription;
		book.AuthorId = vm.AuthorId;
		book.ISBN = vm.ISBN;
		book.Country = vm.Country;
		book.Format = vm.Format;
		book.Genre = vm.Genre;
		book.PageCount = vm.PageCount;
		book.Language = vm.Language;
		book.PuslishedYear = vm.PublishedYear;
		book.RoleOfBook = vm.RoleOfBook;
		book.ShortDescription = vm.ShortDescription;

		if (vm.File != null)
		{
			string newFileName = await vm.File.UploadAsync("wwwroot/imgs/books");
			book.CoverImageUrl = "/imgs/authors/" + newFileName;
		}
		await _repo.SaveAsync();

	}
}
