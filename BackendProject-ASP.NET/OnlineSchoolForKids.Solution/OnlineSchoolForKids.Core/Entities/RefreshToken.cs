using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSchoolForKids.Core.Entities;

public class RefreshToken : BaseEntity
{
	public string UserId { get; set; }
	public string Token { get; set; }
	public DateTime Expires { get; set; }
	public DateTime Created { get; set; }
	public bool IsRevoked { get; set; }
	public ApplicationUser User { get; set; }
}
