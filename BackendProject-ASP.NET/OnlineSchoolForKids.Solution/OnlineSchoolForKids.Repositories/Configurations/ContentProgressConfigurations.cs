using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSchoolForKids.Repository.Configurations
{
	internal class ContentProgressConfigurations : IEntityTypeConfiguration<ContentProgress>
	{
		public void Configure(EntityTypeBuilder<ContentProgress> builder)
		{
			//builder.HasOne(c => c.UserProgress)
			//		.WithMany()
			//		.HasForeignKey(c => c.UserProgressId)
			//		.OnDelete(DeleteBehavior.Restrict);
			
			builder.HasOne(c => c.Content)
					.WithMany()
					.HasForeignKey(c => c.ContentId)
					.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
