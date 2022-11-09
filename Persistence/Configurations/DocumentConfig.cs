using System;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class DocumentConfig: IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> builder)
        {

            builder.HasIndex(x => x.Name).IsUnique();
            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();

            builder.Property(x => x.Description).HasMaxLength(200);
            builder.Property(x => x.CreatedBy).HasMaxLength(150).IsRequired();
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.ModifiedBy).HasMaxLength(150);
            builder.Property(x => x.DocumentURL).HasColumnType("nvarchar(max)").IsRequired();
            builder.Property(x => x.DeletedBy).HasMaxLength(150);
        }
    }
}