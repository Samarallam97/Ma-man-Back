namespace OnlineSchoolForKids.Repositories.Configurations;

internal class ToDoConfigurations : IEntityTypeConfiguration<ToDo>
{
	public void Configure(EntityTypeBuilder<ToDo> builder)
	{
		builder.ToTable("ToDos");

		builder.Property(T => T.Content).IsRequired().HasMaxLength(1000);

		builder.Property(T => T.CreationDate).HasDefaultValueSql("GETUTCDATE()");

		builder.HasOne<User>().WithOne()
			   .HasForeignKey<ToDo>(T => T.UserId)
			   .OnDelete(DeleteBehavior.Cascade);
	}
}