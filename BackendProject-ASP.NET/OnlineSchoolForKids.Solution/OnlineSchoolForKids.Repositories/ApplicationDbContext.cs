using Module = OnlineSchoolForKids.Core.Entities.Module;

namespace OnlineSchoolForKids.Repositories;
public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
	public DbSet<RefreshToken> RefreshTokens { get; set; }
	public DbSet<Category> Categories { get; set; }
	public DbSet<Module> Modules { get; set; }
	public DbSet<Content> Contents { get; set; }
	public DbSet<AgeGroup> AgeGroups { get; set; }
	public DbSet<UserProgress> UserProgress { get; set; }
	public DbSet<ContentProgress> ContentProgress { get; set; }
	public DbSet<Notification> Notifications { get; set; }
	public DbSet<HiddenModule> HiddenModules { get; set; }
	public DbSet<Comment> Comments { get; set; }
	public DbSet<Rating> Ratings { get; set; }
	public DbSet<TODO> TODOs { get; set; }
	public DbSet<Diary> Diaries { get; set; }


	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
	{

	}

	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);

		builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
	}
}
