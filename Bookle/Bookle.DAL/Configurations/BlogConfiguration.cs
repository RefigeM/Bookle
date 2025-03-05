using Bookle.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookle.DAL.Configurations;

public class BlogConfiguration : IEntityTypeConfiguration<Blog>
{
	public void Configure(EntityTypeBuilder<Blog> builder)
	{
		builder.Property(b => b.Title)
					.IsRequired()
					.HasMaxLength(64);
		builder.Property(b => b.Content)
					.IsRequired()
					.HasMaxLength(4000);
	}
}
