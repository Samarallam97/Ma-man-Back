using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSchoolForKids.Core.Models;

public class RegisterModel
{
	[StringLength(200)]
	public string FullName { get; set; }

	[StringLength(100)]
	[EmailAddress]
	public string Email { get; set; }

	[StringLength(100)]
	public string Password { get; set; }

	[Required]
	[Compare("Password")]
	public string ConfirmPassword { get; set; }

	public string Role { get; set; }

	public string? ParentId { get; set; }
}
