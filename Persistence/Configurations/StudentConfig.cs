using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class StudentConfig : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
             builder.HasIndex(x => new{x.Code, x.SchoolId}).IsUnique();
            builder.Property(x => x.Code).HasMaxLength(20).IsRequired();
             builder.Property(x => x.SchoolId).IsRequired();
            builder.Property(x => x.FirstName).HasMaxLength(50).IsRequired();
            builder.Property(x => x.MiddleName).HasMaxLength(50);
            builder.Property(x => x.LastName).HasMaxLength(50).IsRequired();
            builder.Property(x => x.CreatedBy).HasMaxLength(150).IsRequired();
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.DOB).IsRequired();
            builder.Property(x => x.DateJoined).IsRequired();
            builder.Property(x => x.ModifiedBy).HasMaxLength(150);
            builder.Property(x => x.DeletedBy).HasMaxLength(150);
            builder.Property(x => x.ConditionRemarks).HasMaxLength(400);
            builder.Property(x => x.Description).HasMaxLength(400);
        }
    }
}