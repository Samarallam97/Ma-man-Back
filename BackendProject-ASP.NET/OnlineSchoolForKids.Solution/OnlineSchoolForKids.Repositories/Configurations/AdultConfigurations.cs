namespace OnlineSchoolForKids.Repositories.Configurations;

internal class AdultConfigurations : IEntityTypeConfiguration<Adult>
{
    public void Configure(EntityTypeBuilder<Adult> builder)
    {
        builder.ToTable("Adults");

        builder.HasKey(A => A.Id);

		builder.HasOne<User>()
						   .WithOne()
						   .HasForeignKey<Adult>(a => a.Id)
						   .OnDelete(DeleteBehavior.NoAction);

		builder.HasOne<AgeGroup>()
						   .WithMany()
						   .HasForeignKey(a => a.AgeGroupId)
						   .OnDelete(DeleteBehavior.SetNull);
	}
}
