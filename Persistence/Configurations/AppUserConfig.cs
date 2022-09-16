using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class AppUserConfig : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.CreatedBy).IsRequired().HasMaxLength(150);
            builder.Property(x => x.ModifiedBy).HasMaxLength(150);
            builder.Property(x => x.PhoneNumber).HasMaxLength(20);
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.MiddleName).HasMaxLength(50);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Email).IsRequired();
            builder.Property(x => x.UserName).HasMaxLength(20).IsRequired();
            builder.HasIndex(x => x.UserName).IsUnique();
            builder.HasIndex(x => x.Email).IsUnique();
        }
    }
}