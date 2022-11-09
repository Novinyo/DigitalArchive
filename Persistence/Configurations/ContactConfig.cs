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
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.CreatedBy).IsRequired().HasMaxLength(150);
            builder.Property(x => x.ModifiedBy).HasMaxLength(150);
            builder.Property(x => x.DeletedBy).HasMaxLength(150);
            builder.Property(x => x.FatherPhoneNumber).HasMaxLength(20).IsRequired();
            builder.Property(x => x.MotherPhoneNumber).HasMaxLength(20).IsRequired();
            builder.Property(x => x.FatherFirstName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.FatherLastName).HasMaxLength(50);
            builder.Property(x => x.MotherFirstName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.MotherLastName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.FatherEmail).HasMaxLength(256);
            builder.Property(x => x.MotherEmail).HasMaxLength(256);
            builder.Property(x => x.EmergencyContact).HasMaxLength(50).IsRequired();
            builder.Property(x => x.PostalAddress).HasMaxLength(200);
            builder.Property(x => x.StreetAddress).HasMaxLength(200);
        }
    }
}