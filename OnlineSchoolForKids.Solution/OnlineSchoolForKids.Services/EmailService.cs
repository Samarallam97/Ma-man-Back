using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using OnlineSchoolForKids.Core.Models;
using OnlineSchoolForKids.Core.Services.Interfaces;

namespace OnlineSchoolForKids.Services;

public class EmailService : IEmailService
{
	private readonly IConfiguration _configuration;
    public EmailService(IConfiguration configuration)
    {
		_configuration=configuration;
	}
    public async Task SendEmailAsync(Email email)
	{

		var mail = new MimeMessage();
		mail.Sender = MailboxAddress.Parse(_configuration["EmailSettings:Email"]);
		mail.From.Add(new MailboxAddress(_configuration["EmailSettings:DisplayName"], _configuration["EmailSettings:Email"]));
		mail.To.Add(MailboxAddress.Parse(email.To));
		mail.Subject = email.Subject;



		mail.Body = email.IsHTML
				   ? new TextPart("html") { Text = email.Body }
				   : new TextPart("plain") { Text = email.Body };


		using var smtp = new SmtpClient();
		await smtp.ConnectAsync(_configuration["EmailSettings:Host"], int.Parse(_configuration["EmailSettings:Port"]), SecureSocketOptions.StartTls);
		await smtp.AuthenticateAsync(_configuration["EmailSettings:Email"], _configuration["EmailSettings:Password"]);
		await smtp.SendAsync(mail);
		await smtp.DisconnectAsync(true);
	}
}
