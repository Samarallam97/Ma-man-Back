﻿
using OnlineSchoolForKids.API.Helpers;
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

		Services.AddScoped(typeof(IAuthService), typeof(AuthService));

		return Services;
	}

	public static IServiceCollection AddApplicationServices(this IServiceCollection Services)
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

		return Services;
	}
}
