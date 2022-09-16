using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class SchoolConfig : IEntityTypeConfiguration<School>
    {
        public void Configure(EntityTypeBuilder<School> builder)
        {
            builder.HasIndex(x => x.Code).IsUnique();
            builder.Property(x => x.Code).HasMaxLength(20).IsRequired();

            builder.HasIndex(x => x.Name).IsUnique();
            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();

            builder.Property(x => x.Description).HasMaxLength(200);            

            builder.Property(x => x.CreatedBy).HasMaxLength(150).IsRequired();
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.ModifiedBy).HasMaxLength(150);
            builder.Property(x => x.DeletedBy).HasMaxLength(150);
        }
    }
}