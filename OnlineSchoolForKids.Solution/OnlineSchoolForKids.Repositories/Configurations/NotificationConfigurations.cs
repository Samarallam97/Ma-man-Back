using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSchoolForKids.Repository.Configurations
{
	internal class NotificationConfigurations : IEntityTypeConfiguration<Notification>
	{
		public void Configure(EntityTypeBuilder<Notification> builder)
		{
			builder.HasOne(c => c.User)
					.WithMany()
					.HasForeignKey(c => c.UserId)
					.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
