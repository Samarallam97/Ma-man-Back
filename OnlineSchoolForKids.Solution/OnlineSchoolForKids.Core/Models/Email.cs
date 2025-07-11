using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSchoolForKids.Core.Models;

public class Email
{
    [Required]
    public string Subject { get; set; }

	[Required]
    [EmailAddress]
	public string To { get; set; }

    [Required]
    public string Body { get; set; }

	[Required]
	public bool IsHTML { get; set; } = true;
}
