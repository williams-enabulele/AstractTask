using AstractTask.Domain.Entities;
using AstractTask.Infrastruture.Identity;
using AstractTask.Infrastruture.Persistence;
using Microsoft.AspNetCore.Identity;

namespace AstractTask.Infrastruture.Seeder
{
    public class AstractTaskSeeder
    {
        public static async System.Threading.Tasks.Task SeedData(ApplicationContext dbContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await dbContext.Database.EnsureCreatedAsync();

            if (!dbContext.Users.Any())
            {
                List<string> roles = new List<string> { "Admin", "User" };

                foreach (var role in roles)
                {
                    await roleManager.CreateAsync(new IdentityRole { Name = role });
                }

                var user = new ApplicationUser
                {
                    Id = Guid.NewGuid().ToString(),
                    FirstName = "Williams",
                    LastName = "Astract",
                    UserName = "Astract",
                    Email = "test@astract.com",
                    PhoneNumber = "09043546576",
                };
                user.EmailConfirmed = true;
                await userManager.CreateAsync(user, "Password@123");
                await userManager.AddToRoleAsync(user, "Admin");
                await dbContext.TaskCategories.AddRangeAsync(GetTaskCategoryList());
            }

            await dbContext.SaveChangesAsync();
        }

        public static List<TaskCategory> GetTaskCategoryList()
        {
            var TaskCategory = new List<TaskCategory> {
            new TaskCategory{ Id = Guid.NewGuid().ToString(), Name = "Video", Description=""  },
            new TaskCategory{ Id = Guid.NewGuid().ToString(), Name = "Audio", Description="" },
            new TaskCategory{ Id = Guid.NewGuid().ToString(), Name = "Graphics", Description=""}
        };

            return TaskCategory;
        }
    }
}