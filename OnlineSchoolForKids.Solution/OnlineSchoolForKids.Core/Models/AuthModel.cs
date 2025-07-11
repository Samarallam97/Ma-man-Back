using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OnlineSchoolForKids.Core.Models;

public class AuthModel
{
	public string? Message { get; set; }
	public bool IsAuthenticated { get; set; }
	public string? AccessToken { get; set; }
	public DateTime? AccessTokenExpiration { get; set; }

	[JsonIgnore]
	public string? RefreshToken { get; set; }

	public DateTime RefreshTokenExpiration { get; set; }
}
