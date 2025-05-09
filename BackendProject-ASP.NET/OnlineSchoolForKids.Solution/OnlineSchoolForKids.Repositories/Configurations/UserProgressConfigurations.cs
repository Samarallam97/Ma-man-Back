using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSchoolForKids.Repository.Configurations
{
	internal class UserProgressConfigurations : IEntityTypeConfiguration<UserProgress>
	{
		public void Configure(EntityTypeBuilder<UserProgress> builder)
		{
			builder.HasOne(c => c.User)
					.WithMany()
					.HasForeignKey(c => c.UserId)
					.OnDelete(DeleteBehavior.Restrict);

			builder.HasOne(c => c.Module)
					.WithMany()
					.HasForeignKey(c => c.ModuleId)
					.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
