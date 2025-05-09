using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSchoolForKids.Repository.Configurations
{
	internal class RatingConfigurations : IEntityTypeConfiguration<Rating>
	{
		public void Configure(EntityTypeBuilder<Rating> builder)
		{
			//builder.HasOne(c => c.Module)
			//		.WithMany()
			//		.HasForeignKey(c => c.ModuleId)
			//		.OnDelete(DeleteBehavior.Cascade);

			builder.HasOne(c => c.User)
					.WithMany()
					.HasForeignKey(c => c.UserId)
					.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
