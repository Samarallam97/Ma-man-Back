using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OnlineSchoolForKids.API.DTOs.Utilities;
using OnlineSchoolForKids.Core.Models;

namespace OnlineSchoolForKids.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UtilitiesController : ControllerBase
	{
		private readonly IConfiguration _configuration;

		public UtilitiesController(IConfiguration configuration)
        {
			_configuration=configuration;
		}


        [HttpPost("send-email")]
		public async Task<IActionResult> SendEmail([FromBody] Email email)
		{
			IEmailService emailService = new EmailService(_configuration);

			await emailService.SendEmailAsync(email);

			return Ok("Email sent!");
		}

		[HttpPost("send-sms")]
		public async Task<IActionResult> SendSMS([FromBody] SMS sms)
		{
			ISMSService smsService = new SMSService(_configuration);

			var message =  smsService.SendSMS(sms);

			return Ok(new
			{
				Status = message.Status.ToString(),
				Sid = message.Sid,
				To = message.To
			});
		}
	}
}
