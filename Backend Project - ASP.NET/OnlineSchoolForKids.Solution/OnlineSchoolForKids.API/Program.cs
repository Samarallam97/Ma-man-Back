using OnlineSchoolForKids.Repositories;
using OnlineSchoolForKids.Repositories.DataSeeding;

namespace OnlineSchoolForKids.API;

public class Program
{
	public async static Task Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);


		#region DI Services Container

		builder.Services.AddControllers();
		builder.Services.AddEndpointsApiExplorer();

		#region Databases

		builder.Services.AddDbContext<ApplicationDbContext>(options =>
		{
			options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
		});
		#endregion

		builder.Services.AddSwaggerGen(); 
		#endregion

		var app = builder.Build();

		#region Update Databases
		
		var scope = app.Services.CreateScope();
		var serviceProvider = scope.ServiceProvider;

		var ApplicationDbContext = serviceProvider.GetService<ApplicationDbContext>();

		var loggerFactory = serviceProvider.GetService<ILoggerFactory>();

		try
		{
			await ApplicationDbContext.Database.MigrateAsync();
		}
		catch (Exception ex)
		{
			var logger = loggerFactory.CreateLogger<Program>();
			logger.LogError(ex, "An error has occurred while updating the database");
		}
		#endregion

		#region Data Seeding
		await ApplicationSeeder.SeedAsync(ApplicationDbContext);
		#endregion

		#region MiddleWares
		if (app.Environment.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI();
		}

		app.UseHttpsRedirection();

		app.UseAuthorization();

		app.MapControllers(); 
		#endregion

		app.Run();
	}
}
