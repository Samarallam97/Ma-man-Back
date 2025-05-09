using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Module = OnlineSchoolForKids.Core.Entities.Module;

namespace OnlineSchoolForKids.Repository.Configurations
{
	internal class ModuleConfigurations : IEntityTypeConfiguration<Module>
	{
		public void Configure(EntityTypeBuilder<Module> builder)
		{
			builder.HasOne(m => m.CreatedByAdmin)
					.WithMany()
					.HasForeignKey(m => m.CreatedByAdminId)
					.OnDelete(DeleteBehavior.SetNull);

			//builder.HasOne(m => m.Category)
			//		.WithMany()
			//		.HasForeignKey(m => m.CategoryId)
			//		.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
