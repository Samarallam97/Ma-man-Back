﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSchoolForKids.Core.Models;

public class ResetPasswordModel
{
	public string Email { get; set; } = string.Empty;
	public string Token { get; set; } = string.Empty; 
	public string NewPassword { get; set; } = string.Empty;
}
