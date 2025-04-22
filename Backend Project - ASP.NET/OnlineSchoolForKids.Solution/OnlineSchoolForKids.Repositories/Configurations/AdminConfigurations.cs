
namespace OnlineSchoolForKids.Repositories.Configurations;

internal class AdminConfigurations : IEntityTypeConfiguration<Admin>
{
	public void Configure(EntityTypeBuilder<Admin> builder)
	{
		builder.ToTable("Admins");

		builder.HasKey(x => x.Id);

		builder.HasOne<User>()
			   .WithOne()
			   .HasForeignKey<Admin>(a => a.Id)
			   .OnDelete(DeleteBehavior.NoAction);
	}
}
