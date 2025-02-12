using Bookle.Core.Entities;

namespace Bookle.BL.ViewModels.AuthorVMs
{
	public class BookCountOfAuthor
	{
		public int AuthorId { get; set; }
		public string AuthorFullName { get; set; }
		public int BookCount { get; set; }
	}
}
