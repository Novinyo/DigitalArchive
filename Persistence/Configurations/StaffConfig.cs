using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class StaffConfig : IEntityTypeConfiguration<Staff>
    {
        public void Configure(EntityTypeBuilder<Staff> builder)
        {
            builder.Property(x => x.SchoolId).IsRequired();

             builder.HasIndex(x => new{x.Code, x.SchoolId}).IsUnique();
            builder.Property(x => x.Code).HasMaxLength(50).IsRequired();

            builder.Property(x => x.PostalAddress).HasMaxLength(200);
            builder.Property(x => x.StreetAddress).HasMaxLength(200);
            builder.Property(x => x.ConditionRemarks).HasMaxLength(400);
            builder.Property(x => x.Description).HasMaxLength(400);

            builder.Property(x => x.DOB).IsRequired();
            builder.Property(x => x.DateJoined).IsRequired();
        }
    }
}