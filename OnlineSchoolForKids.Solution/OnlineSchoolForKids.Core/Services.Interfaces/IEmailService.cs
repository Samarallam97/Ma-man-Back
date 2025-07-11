using OnlineSchoolForKids.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSchoolForKids.Core.Services.Interfaces;

public interface IEmailService
{
	public Task SendEmailAsync(Email email);
}
