using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSchoolForKids.Repository.Configurations
{
	internal class ParentChildConfigurations : IEntityTypeConfiguration<ParentChild>
	{
		public void Configure(EntityTypeBuilder<ParentChild> builder)
		{
			builder.HasOne(c => c.Parent)
				.WithMany()
				.HasForeignKey(c => c.ParentId)
				.OnDelete(DeleteBehavior.NoAction);

			builder.HasOne(c => c.Child)
					.WithMany()
					.HasForeignKey(c => c.ChildId)
					.OnDelete(DeleteBehavior.NoAction);
		}
	}
}
