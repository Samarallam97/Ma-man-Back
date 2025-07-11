using OnlineSchoolForKids.Core.Models;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace OnlineSchoolForKids.Services;

public class SMSService : ISMSService
{
	private readonly IConfiguration _configuration;

	public SMSService(IConfiguration configuration)
    {
		_configuration=configuration;
	}

    public MessageResource SendSMS(SMS sms)
	{
		var accountSid = _configuration["Twilio:AccountSID"];
		var authToken = _configuration["Twilio:AuthToken"];
		var fromPhone = _configuration["Twilio:PhoneNumber"];

		TwilioClient.Init(accountSid, authToken);

		var message = MessageResource.Create(
			body: sms.Message,
			from: new PhoneNumber(fromPhone),
			to: new PhoneNumber(sms.To)
		);

		return message;
	}
}
