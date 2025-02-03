namespace Bookle.Core.Entities.Common;

public abstract class BaseEntity
{
	public int Id { get; set; }
	public bool IsDeleted { get; set; } = false;
	public DateTime CreatedDate { get; set; } = DateTime.Now;
	public DateTime? UpdatedDate { get; set; }
	public DateTime? DeletedDate { get; set; }
}
