
using Microsoft.Extensions.Configuration;
using OnlineSchoolForKids.API.Helpers;
using OnlineSchoolForKids.Core.Models;
using OnlineSchoolForKids.Core.ServiceInterfaces;
using OnlineSchoolForKids.Service;

namespace OnlineSchoolForKids.API.Extensions;

public static class DependencyInjectionServices
{
	public static IServiceCollection AddIdentityServicesToContainer(this IServiceCollection Services , IConfiguration Configuration)
	{
		
		Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
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
			options.SaveToken = true; // not expired

			//options.RequireHttpsMetadata = true;

			options.TokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuer = true,
				ValidIssuer = Configuration["JWT:Issuer"],
				ValidateAudience = true,
				ValidAudience = Configuration["JWT:Audience"],
				ValidateLifetime = true,
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:SecretKey"]!)),
				ClockSkew = TimeSpan.Zero

			};
		}).AddGoogle(googleOptions =>
		{
			googleOptions.ClientId = Configuration["Authentication:Google:ClientId"];
			googleOptions.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
			googleOptions.CallbackPath = "/auth/login-google";
		});

		Services.AddScoped(typeof(IAuthService), typeof(AuthService));

		return Services;
	}

	public static IServiceCollection AddApplicationServices(this IServiceCollection Services, IConfiguration Configuration)
	{
		Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));

		Services.AddScoped(typeof(ICategoryService), typeof(CategoryService));

		Services.AddScoped(typeof(IModuleService), typeof(ModuleService));
		Services.AddScoped(typeof(IContentService), typeof(ContentService));
		Services.AddScoped(typeof(IDiaryService), typeof(DiaryService));
		Services.AddScoped(typeof(ITODOService), typeof(TODOService));
		Services.AddScoped(typeof(ICommentService), typeof(CommentService));


		Services.AddSingleton(typeof(ICacheService), typeof(CacheService));

		Services.AddAutoMapper(m => m.AddProfile(typeof(MappingProfiles)));


		// Mailkit


		return Services;
	}

	public static IServiceCollection ConfigureSwagger(this IServiceCollection Services)
	{
		Services.AddSwaggerGen(options =>
		{
			// Add the JWT bearer definition
			options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
			{
				Name = "Authorization",
				Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
				Scheme = "Bearer",
				BearerFormat = "JWT",
				In = Microsoft.OpenApi.Models.ParameterLocation.Header,
				Description = "Enter JWT Bearer token **_only_**"
			});

			// Add the requirement to use the scheme globally
			options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
			{
				{
					new Microsoft.OpenApi.Models.OpenApiSecurityScheme
					{
						Reference = new Microsoft.OpenApi.Models.OpenApiReference
						{
							Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
							Id = "Bearer"
						}
					},
					Array.Empty<string>()
				}
			});


		});

		return Services;
	}
}
