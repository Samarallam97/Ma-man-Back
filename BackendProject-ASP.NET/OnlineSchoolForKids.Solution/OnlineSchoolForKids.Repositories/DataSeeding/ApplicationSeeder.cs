using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace OnlineSchoolForKids.Repositories.DataSeeding;

public static class ApplicationSeeder
{
	private static async Task SeedEntity<T>(ApplicationDbContext context, string filePath) where T : class
	{
		if (!context.Set<T>().Any())
		{
			var jsonData = File.ReadAllText(filePath);
			var entities = JsonSerializer.Deserialize<List<T>>(jsonData);

			if (entities != null)
			{
				await context.Set<T>().AddRangeAsync(entities);
				await context.SaveChangesAsync();
			}
		}
	}

	public async static Task SeedAsync(ApplicationDbContext context)
	{
		//await SeedEntity<Role>(context, "../OnlineSchoolForKids.Repositories/DataSeeding/Files/Roles.json");
	}

	public static async Task InitializeRolesAsync(IServiceProvider serviceProvider)
	{
		using var scope = serviceProvider.CreateScope();

		var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
		var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

		string[] roles = new[] { "root_admin", "admin", "parent", "kid", "adult" };

		foreach (var role in roles)
		{
			if (!await roleManager.RoleExistsAsync(role))
			{
				await roleManager.CreateAsync(new IdentityRole(role));
			}
		}

		var rootAdmin = await userManager.FindByEmailAsync("root@admin.com");

		if (rootAdmin == null)
		{
			var user = new ApplicationUser
			{
				UserName = "root_admin",
				Email = "root@admin.com",
				DateOfBirth = new DateTime(2003, 7, 9)
			};

			var result = await userManager.CreateAsync(user, "Root@1234");

			if (result.Succeeded)
			{
				await userManager.AddToRoleAsync(user, "root_admin");
			}
		}
	}
}
