﻿using Bookle.Core.Entities.Common;
using Bookle.Core.Enums;

namespace Bookle.Core.Entities;

public class Book : BaseEntity
{
	public string Title { get; set; }
	public int AuthorId { get; set; }
	public Author Author { get; set; }
	public string? ShortDescription { get; set; }
	public string? Description { get; set; }
	public string? RoleOfBook { get; set; }
	public Genre Genre { get; set; }
	public Format Format { get; set; }
	public string? ISBN { get; set; }
	public string? Country { get; set; }
	public int PuslishedYear { get; set; }
	public int PageCount { get; set; }
	public int Price { get; set; }
	public string Language { get; set; }
	public string? CoverImageUrl { get; set; }
	public ICollection<BookImage>? Images { get; set; }


}
