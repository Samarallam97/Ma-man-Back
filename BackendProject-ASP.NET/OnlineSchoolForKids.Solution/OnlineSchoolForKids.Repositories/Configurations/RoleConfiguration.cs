using OnlineSchoolForKids.Core.Entities;

namespace OnlineSchoolForKids.Repositories.Configurations;

internal class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles");
        builder.Property(R => R.Id).ValueGeneratedOnAdd().UseIdentityColumn();
    }
}
