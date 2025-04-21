using OnlineSchoolForKids.Core.Entities;

namespace OnlineSchoolForKids.Repositories.DataSeeding;

public static class ApplicationSeeder
{
    public async static Task SeedAsync(ApplicationDbContext context)
    {
        if (context.Roles?.Count() == 0)
        {
            var RolesAsString = File.ReadAllText("../OnlineSchoolForKids.Repositories/DataSeeding/Files/Roles.json");
            var roles = JsonSerializer.Deserialize<List<Role>>(RolesAsString);

            foreach (var role in roles)
            {
                context.Roles.Add(role);
            }
        }

        if (context.Users?.Count() == 0)
        {
            var UsersAsString = File.ReadAllText("../OnlineSchoolForKids.Repositories/DataSeeding/Files/Users.json");
            var users = JsonSerializer.Deserialize<List<User>>(UsersAsString);

            foreach (var user in users)
            {
                context.Users.Add(user);
            }
        }

		await context.SaveChangesAsync();
    }

}
