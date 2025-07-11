using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSchoolForKids.Core.Entities;

public class RefreshToken  : BaseEntity
{
	public string Token { get; set; } = string.Empty;

	public DateTime CreatedAt { get; set; }

	public DateTime ExpiresAt { get; set; }
	public bool IsExpired => DateTime.UtcNow >= ExpiresAt;

	public DateTime? RevokedAt { get; set; } // logout
	public bool IsActive => RevokedAt is null && !IsExpired;
}

