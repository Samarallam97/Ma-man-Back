
using Role = OnlineSchoolForKids.Core.Entities.Role;

namespace OnlineSchoolForKids.API.Extensions;

public static class DependencyInjectionServices
{
	public static IServiceCollection AddIdentityServicesToContainer(this IServiceCollection Services , IConfiguration Configuration)
	{
		
		Services.AddIdentity<User, Role>(options =>
		{
			options.Password.RequiredLength = 8;
			options.Password.RequireUppercase = true;
			options.Password.RequireLowercase = true;
			options.Password.RequireNonAlphanumeric = true;
			options.Password.RequireDigit = true;

		}).AddEntityFrameworkStores<ApplicationDbContext>()
		  .AddDefaultTokenProviders();


		Services.AddAuthentication(options =>
		{
			options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
		}).AddJwtBearer(options =>
		{
			options.TokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuer = true,
				ValidIssuer = Configuration["JWT:Issuer"],
				ValidateAudience = false,
				ValidateLifetime = true,
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:SecretKey"]!))

			};
		});

		Services.AddAuthorization(options =>
		{
			options.AddPolicy("RootPolicy", policy => policy.RequireRole("Root"));
			options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
			options.AddPolicy("ParentPolicy", policy => policy.RequireRole("Parent"));
			options.AddPolicy("AdultPolicy", policy => policy.RequireRole("Adult"));
			options.AddPolicy("KidPolicy", policy => policy.RequireRole("Kid"));

		});

		Services.AddScoped(typeof(IAuthenticationService) , typeof(AuthenticationService));

		return Services;
	}

	public static IServiceCollection AddApplicationServices(this IServiceCollection Services)
	{
		Services.AddSingleton(typeof(ICacheService), typeof(CacheService));
		Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));

		return Services;
	}
}
