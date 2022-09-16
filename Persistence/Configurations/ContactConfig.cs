using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class ContactConfig : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.Property(x => x.Code).HasMaxLength(50).IsRequired();
            builder.HasIndex(x => new { x.Code, x.Email }).IsUnique();
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.CreatedBy).IsRequired().HasMaxLength(150);
            builder.Property(x => x.ModifiedBy).HasMaxLength(150);
            builder.Property(x => x.DeletedBy).HasMaxLength(150);
            builder.Property(x => x.PhoneNumber).HasMaxLength(20).IsRequired();
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.MiddleName).HasMaxLength(50);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Email).HasMaxLength(256)
            .IsRequired();
            builder.HasIndex(x => x.Email).IsUnique();
            builder.Property(x => x.PostalAddress).HasMaxLength(200);
            builder.Property(x => x.StreetAddress).HasMaxLength(200);
        }
    }
}