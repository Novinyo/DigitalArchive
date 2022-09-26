using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class StaffTypeConfig : IEntityTypeConfiguration<StaffType>
    {
        public void Configure(EntityTypeBuilder<StaffType> builder)
        {
            builder.Property(x => x.SchoolId);

            builder.Property(x => x.Code).HasMaxLength(50).IsRequired();
            builder.HasIndex(x => new { x.SchoolId, x.Code }).IsUnique();

            builder.HasIndex(x => new { x.SchoolId, x.Name }).IsUnique();
            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();

            builder.Property(x => x.Description).HasMaxLength(200);
            builder.Property(x => x.CreatedBy).HasMaxLength(150).IsRequired();
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.ModifiedBy).HasMaxLength(150);
            builder.Property(x => x.DeletedBy).HasMaxLength(150);
        }
    }
}