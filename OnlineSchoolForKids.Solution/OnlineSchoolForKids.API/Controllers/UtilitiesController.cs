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
		public async Task<IActionResult> SendTestEmail([FromBody] EmailDto emailDto)
		{
			IEmailService emailService = new EmailService(_configuration);

			await emailService.SendEmailAsync(

				new Email()
				{
					Body = emailDto.Body,
					To = emailDto.To,
					Subject = emailDto.Subject,
					IsHTML = emailDto.IsHTML
				}
			);

			return Ok("Email sent!");
		}
	}
}
