namespace OnlineSchoolForKids.API;

public class Program
{
	public async static Task Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		#region DI Services Container

		builder.Services.AddControllers().AddJsonOptions(options =>
		{
			options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
		}); ;

		builder.Services.AddEndpointsApiExplorer();

		#region Databases

		builder.Services.AddDbContext<ApplicationDbContext>(options =>
		{
			options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
		});

		// Redis
		builder.Services.AddSingleton<IConnectionMultiplexer>(provider =>
		{
			var connectionString = builder.Configuration.GetConnectionString("Redis");
			return ConnectionMultiplexer.Connect(connectionString !);
		});
		#endregion

		builder.Services.AddIdentityServicesToContainer(builder.Configuration);
		builder.Services.AddApplicationServices();

		#region Cors Policy
		builder.Services.AddCors(options =>
		{
			options.AddPolicy("MyPolicy", options =>
			{
				options.WithOrigins(builder.Configuration["FrontBaseUrl"] !)
				.AllowAnyHeader()
				.AllowAnyMethod()
				.AllowCredentials();
			});
		}); 
		#endregion

		#region Validation Error Handling

		builder.Services.Configure<ApiBehaviorOptions>(options =>
		{
			options.InvalidModelStateResponseFactory = (actionContext) =>
			{
				var errors = actionContext.ModelState.Where(p => p.Value?.Errors.Count() > 0)
													.SelectMany(p => p.Value?.Errors!)
													.Select(e => e.ErrorMessage)
													.ToArray();

				var validationErrorResponse = new ValidationErrorResponse()
				{
					Errors = errors
				};

				return new BadRequestObjectResult(validationErrorResponse);
			};
		});
		
		#endregion

		builder.Services.AddSwaggerGen();
		#endregion

		var app = builder.Build();

		#region Update Database
		
		var scope = app.Services.CreateScope();
		var serviceProvider = scope.ServiceProvider;

		var ApplicationDbContext = serviceProvider.GetService<ApplicationDbContext>() !;

		var loggerFactory = serviceProvider.GetService<ILoggerFactory>() !;

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
		//await ApplicationSeeder.SeedAsync(ApplicationDbContext!);
		await ApplicationSeeder.InitializeRolesAsync(serviceProvider);
		#endregion

		#region MiddleWares

		#region Exception Handling

		app.UseMiddleware<ExceptionHandlingMiddleWare>(); 

		#endregion


		app.UseSwagger();
		app.UseSwaggerUI();
		

		app.UseHttpsRedirection();

		#region Not Found EndPoint Handling

		app.UseStatusCodePagesWithReExecute("/error/{0}");

		#endregion

		#region Cors Policy

		app.UseCors("MyPolicy"); 

		#endregion

		app.UseAuthentication();
		app.UseAuthorization();

		app.MapControllers(); 

		#endregion

		app.Run();
	}
}
