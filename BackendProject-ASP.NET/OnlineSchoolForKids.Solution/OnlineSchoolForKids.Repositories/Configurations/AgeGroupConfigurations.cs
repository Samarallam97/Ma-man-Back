using OnlineSchoolForKids.Core.Entities;

namespace OnlineSchoolForKids.Repositories.Configurations;

internal class AgeGroupConfigurations : IEntityTypeConfiguration<AgeGroup>
{
    public void Configure(EntityTypeBuilder<AgeGroup> builder)
    {
        builder.ToTable("AgeGroups");

        builder.Property(A => A.Name).IsRequired().HasMaxLength(60);

	}
}
