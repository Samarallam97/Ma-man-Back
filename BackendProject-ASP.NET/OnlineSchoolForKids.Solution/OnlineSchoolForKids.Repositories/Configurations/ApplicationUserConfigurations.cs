using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSchoolForKids.Repository.Configurations;

internal class ApplicationUserConfigurations : IEntityTypeConfiguration<ApplicationUser>
{
	public void Configure(EntityTypeBuilder<ApplicationUser> builder)
	{
		builder.Property(U => U.CreationDate).HasDefaultValueSql("GETUTCDATE()");

		//builder.HasOne(c => c.AgeGroup)
		//		.WithMany()
		//		.HasForeignKey(c => c.AgeGroupId)
		//		.OnDelete(DeleteBehavior.Restrict);
	}
}
