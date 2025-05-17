using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSchoolForKids.Repository.Configurations
{
	internal class CategoryConfigurations : IEntityTypeConfiguration<Category>
	{
		public void Configure(EntityTypeBuilder<Category> builder)
		{
			builder.HasOne(c => c.CreatedByAdmin)
					.WithMany()
					.HasForeignKey(c => c.CreatedByAdminId)
					.OnDelete(DeleteBehavior.SetNull);
		}
	}
}
