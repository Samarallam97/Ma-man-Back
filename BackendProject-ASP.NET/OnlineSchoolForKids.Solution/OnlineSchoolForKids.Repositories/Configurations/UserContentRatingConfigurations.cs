namespace OnlineSchoolForKids.Repositories.Configurations;

internal class UserContentRatingConfigurations : IEntityTypeConfiguration<UserContentRating>
{
	public void Configure(EntityTypeBuilder<UserContentRating> builder)
	{
		builder.HasKey(uc => new { uc.ContentId, uc.UserId });


		builder.Property(uc => uc.RatingDate).HasDefaultValueSql("GETUTCDATE()");


		builder.HasOne(uc => uc.User)
			   .WithMany(u => u.ContentsRated)
			   .HasForeignKey(uc => uc.UserId);


		builder.HasOne(uc => uc.Content)
	   .WithMany(c => c.UsersRated)
	   .HasForeignKey(uc => uc.ContentId);
	}
}
