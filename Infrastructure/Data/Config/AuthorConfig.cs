using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config;

internal class AuthorConfig : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.Property(x => x.FirstName).HasColumnType("nvarchar(50)")
            .HasDefaultValue("unknown");
        builder.Property(x => x.LastName).HasColumnType("nvarchar(50)")
            .HasDefaultValue("unknown");
    }
}
