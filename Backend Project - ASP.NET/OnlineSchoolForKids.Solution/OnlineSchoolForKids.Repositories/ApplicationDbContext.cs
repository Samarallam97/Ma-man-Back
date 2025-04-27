namespace OnlineSchoolForKids.Repositories;
public class ApplicationDbContext : IdentityDbContext<User, Role, int>
{
    public DbSet<Parent> Parents { get; set; }
    public DbSet<Kid> Kids { get; set; }
    public DbSet<Adult> Adults { get; set; }
	public DbSet<Admin> Admins { get; set; }
	public DbSet<AgeGroup> AgeGroups { get; set; }
	public DbSet<Format> Formats { get; set; }
	public DbSet<Category> Categories { get; set; }
	public DbSet<Content> Content { get; set; }
	public DbSet<Content> Comments { get; set; }
	public DbSet<ToDo> ToDos { get; set; }
	public DbSet<Diary> Diaries { get; set; }

	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
