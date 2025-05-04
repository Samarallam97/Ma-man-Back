using OnlineSchoolForKids.Core.Entities;

namespace OnlineSchoolForKids.Repositories.Configurations;

internal class ParentConfigurations : IEntityTypeConfiguration<Parent>
{
    public void Configure(EntityTypeBuilder<Parent> builder)
    {
        builder.ToTable("Parents");

        builder.HasKey(P => P.Id);

		builder.HasOne(p => p.User)
						   .WithOne()
						   .HasForeignKey<Parent>(p => p.Id)
						   .OnDelete(DeleteBehavior.NoAction);
	}
}
