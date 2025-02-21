using Bookle.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookle.DAL.Configurations
{
	public class BookConfiguration : IEntityTypeConfiguration<Book>
	{
		public void Configure(EntityTypeBuilder<Book> builder)
		{
			builder.Property(b => b.Title)
				.IsRequired()
				.HasMaxLength(64);

			builder.Property(b => b.ShortDescription)
			   .HasMaxLength(500);

			builder.Property(b => b.Description)
			   .HasColumnType("TEXT");

			builder.Property(b => b.RoleOfBook)
				.HasMaxLength(255);

			builder.Property(b => b.ISBN)
				.HasMaxLength(20);

			builder.Property(b => b.PublishingCountry)
				.HasMaxLength(100);

			builder.Property(b => b.PublishedYear)
				.IsRequired();

			builder.Property(b => b.PageCount)
				.IsRequired();

			builder.Property(b => b.Price)
				.IsRequired()
				.HasColumnType("decimal(18,2)"); 

			builder.Property(b => b.Language)
				.IsRequired()
				.HasMaxLength(50);

			builder.Property(b => b.CoverImageUrl)
				.HasMaxLength(500);

			builder.HasMany(b => b.BookRatings)
		.WithOne(br => br.Book)
		.HasForeignKey(br => br.BookId)
		.OnDelete(DeleteBehavior.Cascade);

            builder.Property(b => b.RoleOfBook)
        .HasMaxLength(2000);

            builder.Property(b => b.Description)
       .HasMaxLength(1500);
			
			builder.Property(b => b.ShortDescription)
        .HasMaxLength(500);

        }
	}
}
