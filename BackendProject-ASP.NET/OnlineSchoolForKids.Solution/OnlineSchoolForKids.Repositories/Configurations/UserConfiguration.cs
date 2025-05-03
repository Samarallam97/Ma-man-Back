using OnlineSchoolForKids.Core.Entities;

namespace OnlineSchoolForKids.Repositories.Configurations;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.Property(U => U.Id).ValueGeneratedOnAdd().UseIdentityColumn();
        builder.Property(U => U.FirstName).IsRequired().HasMaxLength(50);
        builder.Property(U => U.LastName).IsRequired().HasMaxLength(50);
        builder.Property(U => U.PictureURL).IsRequired(false);
        builder.Property(U => U.CreationDate).HasDefaultValueSql("GETUTCDATE()");


	}
}
