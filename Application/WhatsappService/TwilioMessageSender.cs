using Twilio.Types;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Microsoft.Extensions.Options;

namespace Chatbot.Application.WhatsappService
{
    public class TwilioMessageSender
    {
        private readonly TwilioSettings _settings;
        public TwilioMessageSender(IOptions<TwilioSettings> settings)
        {
            _settings = settings.Value;
            TwilioClient.Init(_settings.AccountSid, _settings.AuthToken);
        }

        public void SendTemplateMessage(string toPhoneNumber, string contentSid, string variablesJson)
        {
            var messageOptions = new CreateMessageOptions(
                new PhoneNumber($"whatsapp:{toPhoneNumber}"))
            {
                From = new PhoneNumber(_settings.FromPhoneNumber),
                ContentSid = contentSid,
                ContentVariables = variablesJson
            };

            var message = MessageResource.Create(messageOptions);
            Console.WriteLine($"Mensagem enviada! SID: {message.Sid}");
        }
    }
}

