
namespace OnlineSchoolForKids.Repositories.Configurations
{
    internal class KidConfingurations : IEntityTypeConfiguration<Kid>
    {
        public void Configure(EntityTypeBuilder<Kid> builder)
        {
            builder.ToTable("Kids");

            builder.HasKey(K => K.Id);


			builder.HasOne(k => k.User)
				   .WithOne()
				   .HasForeignKey<Kid>(k => k.Id)
				   .OnDelete(DeleteBehavior.NoAction);

			builder.HasOne(k => k.Parent)
				   .WithMany(p => p.Kids)
				   .HasForeignKey(k => k.ParentId)
				   .OnDelete(DeleteBehavior.Cascade);


			builder.HasOne(k => k.AgeGroup)
					.WithMany(a => a.Kids)
					.HasForeignKey(k => k.AgeGroupId)
					.OnDelete(DeleteBehavior.SetNull);
		}
    }
}
