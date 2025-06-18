using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSchoolForKids.Core.Entities;

public class RefreshToken : BaseEntity
{
	public string Token { get; set; } = string.Empty;

	public string UserId { get; set; } = string.Empty;

	public ApplicationUser User { get; set; } = null!;

	public DateTime CreatedAt { get; set; }

	public DateTime ExpiresAt { get; set; }

	public DateTime? RevokedAt { get; set; } // logout

	public bool IsRevoked => RevokedAt.HasValue; 

	public bool IsExpired => DateTime.UtcNow >= ExpiresAt;

	public bool IsActive => !IsRevoked && !IsExpired;
}

