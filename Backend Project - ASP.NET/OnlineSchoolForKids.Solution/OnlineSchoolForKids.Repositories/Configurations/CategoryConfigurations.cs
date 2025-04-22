namespace OnlineSchoolForKids.Repositories.Configurations;

internal class CategoryConfigurations : IEntityTypeConfiguration<Category>
{
	public void Configure(EntityTypeBuilder<Category> builder)
	{
		builder.ToTable("Categories");

		builder.Property(C => C.Name).IsRequired().HasMaxLength(60);
		builder.Property(C => C.Description).IsRequired().HasMaxLength(1000);
	}
}
