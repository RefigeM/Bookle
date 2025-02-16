using Bookle.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookle.DAL.Configurations;

public class BookRatingConfiguration : IEntityTypeConfiguration<BookRating>
{
	public void Configure(EntityTypeBuilder<BookRating> builder)
	{
		builder.HasOne(br => br.Book)
			.WithMany(b => b.BookRatings)
			.HasForeignKey(br => br.BookId)
			.OnDelete(DeleteBehavior.Cascade);
	}
}
