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

		builder.HasOne(c => c.Category)
			   .WithMany(c => c.Contents)
			   .HasForeignKey(C => C.CategoryId)
			   .OnDelete(DeleteBehavior.SetNull);

		builder.HasOne(c => c.Format)
			   .WithMany(f => f.Contents)
			   .HasForeignKey(C => C.FormatId)
			   .OnDelete(DeleteBehavior.Restrict);

		builder.HasOne(c => c.Admin)
			   .WithMany(a => a.Contents)
			   .HasForeignKey(C => C.AdminId)
			   .OnDelete(DeleteBehavior.SetNull);

	}
}
