
namespace OnlineSchoolForKids.Repositories.Configurations;

internal class DiaryConfigurations : IEntityTypeConfiguration<Diary>
{
	public void Configure(EntityTypeBuilder<Diary> builder)
	{
		builder.ToTable("Diaries");

		builder.Property(T => T.Content).IsRequired().HasMaxLength(10000);

		builder.Property(T => T.CreationDate).HasDefaultValueSql("GETUTCDATE()");

		builder.HasOne<User>().WithOne()
			   .HasForeignKey<Diary>(T => T.UserId)
			   .OnDelete(DeleteBehavior.Cascade);
	}
}
