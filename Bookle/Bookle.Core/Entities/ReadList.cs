using Bookle.Core.Entities.Common;

namespace Bookle.Core.Entities
{
	public class ReadList : BaseEntity
	{
		public string UserId { get; set; }
		public User User { get; set; }

		public int BookId { get; set; }
		public Book Book { get; set; }
		public bool IsReaded { get; set; } = false;
    }
}
