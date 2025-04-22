using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSchoolForKids.Repositories.Configurations
{
	internal class CommentConfigurations : IEntityTypeConfiguration<Comment>
	{
		public void Configure(EntityTypeBuilder<Comment> builder)
		{
			builder.ToTable("Comments");

			builder.Property(C => C.Content).IsRequired().HasMaxLength(1000);
			builder.Property(C => C.Date).HasDefaultValueSql("GETUTCDATE()");

			builder.HasOne<User>().WithOne()
				   .HasForeignKey<Comment>(C => C.UserId)
				   .OnDelete(DeleteBehavior.Cascade);

			builder.HasOne<Content>().WithOne()
			   .HasForeignKey<Comment>(C => C.ContentId)
			   .OnDelete(DeleteBehavior.Cascade);
		}
	}
}
