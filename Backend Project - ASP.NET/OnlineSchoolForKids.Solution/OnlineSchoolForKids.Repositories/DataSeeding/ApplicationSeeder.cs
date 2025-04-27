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
		await SeedEntity<Role>(context, "../OnlineSchoolForKids.Repositories/DataSeeding/Files/Roles.json");
		await SeedEntity<User>(context, "../OnlineSchoolForKids.Repositories/DataSeeding/Files/Users.json");
		await SeedEntity<Admin>(context, "../OnlineSchoolForKids.Repositories/DataSeeding/Files/Admins.json");
		await SeedEntity<Parent>(context, "../OnlineSchoolForKids.Repositories/DataSeeding/Files/Parents.json");
		await SeedEntity<AgeGroup>(context, "../OnlineSchoolForKids.Repositories/DataSeeding/Files/AgeGroups.json");
		await SeedEntity<Adult>(context, "../OnlineSchoolForKids.Repositories/DataSeeding/Files/Adults.json");
		await SeedEntity<Kid>(context, "../OnlineSchoolForKids.Repositories/DataSeeding/Files/Kids.json");
		await SeedEntity<Category>(context, "../OnlineSchoolForKids.Repositories/DataSeeding/Files/Categories.json");
		await SeedEntity<Format>(context, "../OnlineSchoolForKids.Repositories/DataSeeding/Files/Formats.json");
		await SeedEntity<Content>(context, "../OnlineSchoolForKids.Repositories/DataSeeding/Files/Content.json");
	}
}

#region Re factored
		
//public async static Task SeedAsync(ApplicationDbContext context)
//    {
//        if (context.Roles?.Count() == 0)
//        {
//            var RolesAsString = File.ReadAllText("../OnlineSchoolForKids.Repositories/DataSeeding/Files/Roles.json");
//            var roles = JsonSerializer.Deserialize<List<Role>>(RolesAsString);

//            foreach (var role in roles)
//            {
//                context.Roles.Add(role);
//            }
//        }

//        if (context.Users?.Count() == 0)
//        {
//            var UsersAsString = File.ReadAllText("../OnlineSchoolForKids.Repositories/DataSeeding/Files/Users.json");
//            var users = JsonSerializer.Deserialize<List<User>>(UsersAsString);

//            foreach (var user in users)
//            {
//                context.Users.Add(user);
//            }
//        }

//		if (context.Admins?.Count() == 0)
//		{
//			var adminsAsString = File.ReadAllText("../OnlineSchoolForKids.Repositories/DataSeeding/Files/Admins.json");
//			var admins = JsonSerializer.Deserialize<List<Admin>>(adminsAsString);

//			foreach (var admin in admins)
//			{
//				context.Admins.Add(admin);
//			}
//		}

//		if (context.Parents?.Count() == 0)
//		{
//			var parentsAsString = File.ReadAllText("../OnlineSchoolForKids.Repositories/DataSeeding/Files/Parents.json");
//			var parents = JsonSerializer.Deserialize<List<Parent>>(parentsAsString);

//			foreach (var parent in parents)
//			{
//				context.Parents.Add(parent);
//			}
//		}


//		if (context.AgeGroups?.Count() == 0)
//		{
//			var AgeGroupsAsString = File.ReadAllText("../OnlineSchoolForKids.Repositories/DataSeeding/Files/AgeGroups.json");
//			var AgeGroups = JsonSerializer.Deserialize<List<AgeGroup>>(AgeGroupsAsString);

//			foreach (var AgeGroup in AgeGroups)
//			{
//				context.AgeGroups.Add(AgeGroup);
//			}
//		}

//		if (context.Adults?.Count() == 0)
//		{
//			var AdultsAsString = File.ReadAllText("../OnlineSchoolForKids.Repositories/DataSeeding/Files/Adults.json");
//			var Adults = JsonSerializer.Deserialize<List<Adult>>(AdultsAsString);

//			foreach (var Adult in Adults)
//			{
//				context.Adults.Add(Adult);
//			}
//		}

//		if (context.Kids?.Count() == 0)
//		{
//			var KidsAsString = File.ReadAllText("../OnlineSchoolForKids.Repositories/DataSeeding/Files/Kids.json");
//			var Kids = JsonSerializer.Deserialize<List<Kid>>(KidsAsString);

//			foreach (var Kid in Kids)
//			{
//				context.Kids.Add(Kid);
//			}
//		}


//		if (context.Categories?.Count() == 0)
//		{
//			var categoriesAsString = File.ReadAllText("../OnlineSchoolForKids.Repositories/DataSeeding/Files/Categories.json");
//			var categories = JsonSerializer.Deserialize<List<Category>>(categoriesAsString);

//			foreach (var category in categories)
//			{
//				context.Categories.Add(category);
//			}
//		}

//		if (context.Formats?.Count() == 0)
//		{
//			var formatsAsString = File.ReadAllText("../OnlineSchoolForKids.Repositories/DataSeeding/Files/Formats.json");
//			var formats = JsonSerializer.Deserialize<List<Format>>(formatsAsString);

//			foreach (var format in formats)
//			{
//				context.Formats.Add(format);
//			}
//		}


//		if (context.Content?.Count() == 0)
//		{
//			var contentAsString = File.ReadAllText("../OnlineSchoolForKids.Repositories/DataSeeding/Files/Content.json");
//			var content = JsonSerializer.Deserialize<List<Content>>(contentAsString);

//			foreach (var entry in content)
//			{
//				context.Content.Add(entry);
//			}
//		}

//		await context.SaveChangesAsync();
//    } 
#endregion