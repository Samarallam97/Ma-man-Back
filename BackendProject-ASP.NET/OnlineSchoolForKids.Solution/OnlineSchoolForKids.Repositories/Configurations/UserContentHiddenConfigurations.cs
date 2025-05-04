using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSchoolForKids.Repositories.Configurations;

public class UserContentHiddenConfigurations : IEntityTypeConfiguration<UserContentHidden>
{
	public void Configure(EntityTypeBuilder<UserContentHidden> builder)
	{
		builder.HasKey(uc => new { uc.ContentId, uc.UserId });


		builder.HasOne(uc => uc.User)
			   .WithMany(u => u.ContentsHidden)
			   .HasForeignKey(uc => uc.UserId);


		builder.HasOne(uc => uc.Content)
	   .WithMany(c => c.UsersHidden)
	   .HasForeignKey(uc => uc.ContentId);
	}
}
