using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSchoolForKids.Repository.Configurations
{
	internal class ContentConfigurations : IEntityTypeConfiguration<Content>
	{
		public void Configure(EntityTypeBuilder<Content> builder)
		{
			builder.HasOne(c => c.CreatedByAdmin)
				.WithMany()
				.HasForeignKey(c => c.CreatedByAdminId)
				.OnDelete(DeleteBehavior.SetNull);

			//builder.HasOne(c => c.Module)
			//		.WithMany()
			//		.HasForeignKey(c => c.ModuleId)
			//		.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
