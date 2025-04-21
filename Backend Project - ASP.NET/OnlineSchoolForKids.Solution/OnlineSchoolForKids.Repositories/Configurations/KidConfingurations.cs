using OnlineSchoolForKids.Core.Entities;

namespace OnlineSchoolForKids.Repositories.Configurations
{
    internal class KidConfingurations : IEntityTypeConfiguration<Kid>
    {
        public void Configure(EntityTypeBuilder<Kid> builder)
        {
            builder.ToTable("Kids");

            builder.HasKey(K => K.Id);


			builder.HasOne<User>().WithOne()
				   .HasForeignKey<Kid>(k => k.Id)
				   .OnDelete(DeleteBehavior.NoAction);

			builder.HasOne<Parent>()
				   .WithMany()
				   .HasForeignKey(k => k.ParentId)
				   .OnDelete(DeleteBehavior.Cascade);


			builder.HasOne<AgeGroup>()
							   .WithMany()
							   .HasForeignKey(k => k.AgeGroupId)
							   .OnDelete(DeleteBehavior.SetNull);
		}
    }
}
