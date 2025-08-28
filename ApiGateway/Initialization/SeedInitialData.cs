using DataLayer.Models;
using Microsoft.AspNetCore.Identity;

namespace ApiGateway.Initialization
{
    public static class SeedInitialData
    {
        public static async Task SeedRolesAndSuperAdmin(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            string[] roles = new[] { "Admin", "Client", "SuperAdmin" };
            foreach (string role in roles)
            {
                var roleExists = await roleManager.RoleExistsAsync(role);
                if (!roleExists)
                {
                    await roleManager.CreateAsync(new IdentityRole
                    {
                        Name = role,
                        NormalizedName = role.ToUpper()
                    });
                }
            }
            var adminUser = await userManager.FindByEmailAsync("SuperAdmin@super.com");
            if (adminUser == null)
            {
                adminUser = new User
                {
                    FirstName = "Super",
                    LastName = "Admin",
                    Email = "SuperAdmin@super.com",
                    UserName = "__superadmin__"
                };
                var createUserResult = await userManager.CreateAsync(adminUser, "A123!!!!!");
                if (!createUserResult.Succeeded)
                {
                    throw new Exception($"User creation failed: {string.Join(", ", createUserResult.Errors.Select(e => e.Description))}");
                }
                var addRoleResult = await userManager.AddToRoleAsync(adminUser, "SuperAdmin");
                if (!addRoleResult.Succeeded)
                {
                    throw new Exception($"Role assignment failed: {string.Join(", ", addRoleResult.Errors.Select(e => e.Description))}");
                }
            }
        }


    }
}
