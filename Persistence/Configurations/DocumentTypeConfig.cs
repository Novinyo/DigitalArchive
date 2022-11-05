using System;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class DocumentTypeConfig: IEntityTypeConfiguration<DocumentType>
    {
        public void Configure(EntityTypeBuilder<DocumentType> builder)
        {
            builder.Property(x => x.Code).HasMaxLength(50).IsRequired();
            builder.HasIndex(x => new {x.Code}).IsUnique();

            builder.HasIndex(x => new {x.Name}).IsUnique();
            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(200);
            builder.Property(x => x.CreatedBy).HasMaxLength(150).IsRequired();
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.ModifiedBy).HasMaxLength(150);
            builder.Property(x => x.DeletedBy).HasMaxLength(150);
        }
    }
}