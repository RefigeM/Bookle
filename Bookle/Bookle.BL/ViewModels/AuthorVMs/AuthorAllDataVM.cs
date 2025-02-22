using Bookle.Core.Entities;

namespace Bookle.BL.ViewModels.AuthorVMs
{
	public class AuthorAllDataVM
	{
		public int AuthorId { get; set; }
		public string AuthorName { get; set; }
		public string AuthorImg { get; set; }
        public string Biography { get; set; }

        public int BookCount { get; set; }
        public string Country { get; set; }
        public string FacebookUrl { get; set; }
        public string TwitterUrl { get; set; }
        public string InstagramUrl { get; set; }
        public string LinkedInUrl { get; set; }
        public int? BirthYear { get; set; }
        public int? DeathYear { get; set; }
        public List<Book> Books { get; set; } = new List<Book>();


    }

}