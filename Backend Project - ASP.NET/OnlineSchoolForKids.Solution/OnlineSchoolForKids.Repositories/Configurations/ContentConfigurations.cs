namespace OnlineSchoolForKids.Repositories.Configurations;

internal class ContentConfigurations : IEntityTypeConfiguration<Content>
{
	public void Configure(EntityTypeBuilder<Content> builder)
	{
		builder.ToTable("Content");

		builder.Property(C => C.Title).IsRequired().HasMaxLength(60);
		builder.Property(C => C.Description).IsRequired().HasMaxLength(1000);
		builder.Property(C => C.URL).IsRequired().HasMaxLength(1000);

		builder.Property(C => C.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

		builder.HasOne<Category>().WithMany()
			    .HasForeignKey(C => C.CategoryId)
				.OnDelete(DeleteBehavior.SetNull);

		builder.HasOne<Format>().WithMany()
			   .HasForeignKey(C => C.FormatId)
			   .OnDelete(DeleteBehavior.SetNull);

		builder.HasOne<Admin>().WithMany()
			   .HasForeignKey(C => C.AdminId)
			   .OnDelete(DeleteBehavior.SetNull);

		builder.HasMany<AgeGroup>().WithMany();

		builder.HasMany<User>().WithMany();

		builder.HasMany<User>().WithMany();
	}
}
