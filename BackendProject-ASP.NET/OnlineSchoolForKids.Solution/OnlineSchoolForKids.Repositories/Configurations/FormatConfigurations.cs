namespace OnlineSchoolForKids.Repositories.Configurations;

internal class FormatConfigurations : IEntityTypeConfiguration<Format>
{
	public void Configure(EntityTypeBuilder<Format> builder)
	{
		builder.ToTable("Formats");

		builder.Property(F => F.Name).IsRequired().HasMaxLength(20);
	}
}
