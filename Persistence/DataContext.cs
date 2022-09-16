using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Reflection;

namespace Persistence
{
    public class DataContext:IdentityDbContext<AppUser>
    {
        public DataContext(DbContextOptions options): base(options)
        {
            
        }
       public DbSet<DocumentType> DocumentTypes { get; set; }
       public DbSet<Document> Documents { get; set; }
        public DbSet<SchoolType> SchoolTypes { get; set; }
        public DbSet<EntityType> EntityTypes { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
            builder.Entity<AppUser>(e => {
                e.ToTable("Users");
            });
              builder.Entity<IdentityRole>(e => {
                e.ToTable("Roles");
            });
              builder.Entity<IdentityUserRole<string>>(e => {
                e.ToTable("UserRoles");
            });
             builder.Entity<IdentityUserClaim<string>>(e => {
                e.ToTable("UserClaims");
            });
            builder.Entity<IdentityUserLogin<string>>(e => {
                e.ToTable("UserLogins");
            });
            builder.Entity<IdentityRoleClaim<string>>(e => {
                e.ToTable("RoleClaims");
            });
             builder.Entity<IdentityUserToken<string>>(e => {
                e.ToTable("UserTokens");
            });
        }
    }
}