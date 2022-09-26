using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Persistence
{
    public class Seed
    {

        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.Roles.Any())
            {
                var roles = systemRoles();

                foreach (var role in roles)
                {
                    Console.WriteLine(role);
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
        public static async Task SeedData(DataContext context, UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var admin = new AppUser
                {
                    FirstName = "Freeman",
                    LastName = "Novinyo",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    UserName = "novinyo",
                    Email = "novinyo@test.com"
                };

                admin.CreatedAt = DateTime.UtcNow;
                admin.CreatedBy = admin.Id;

                await userManager.CreateAsync(admin, "Demo@12345_");
                var roles = systemRoles();
                await userManager.AddToRolesAsync(admin, roles);
            }
        }

        private static IEnumerable<string> systemRoles()
        {
            return Enum.GetValues(typeof(Roles)).Cast<Roles>().Select(r => r.ToString());
        }
    }
}