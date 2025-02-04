using Bookle.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookle.DAL.Configurations;

public class BookImageConfiguration : IEntityTypeConfiguration<BookImage>
{
	public void Configure(EntityTypeBuilder<BookImage> builder)
	{
		builder.Property(bi => bi.ImageUrl)
				   .HasMaxLength(500);


		builder.HasOne(bi => bi.Book)
			.WithMany(b => b.Images) 
			.HasForeignKey(bi => bi.BookId)
			.OnDelete(DeleteBehavior.Cascade);
	}
}
