using Chatbot.Application.DTOs;
using Chatbot.Application.Interaction;
using Chatbot.Application.WhatsappService;
using Microsoft.AspNetCore.Mvc;

namespace Chatbot.Presentation.Controllers
{
    public class ChatbotController : ControllerBase
    {

        [HttpPost("webhook")]
        public async Task<IActionResult> ReceiveMessage(
            [FromBody] TwilioMessageDto message,
            [FromServices] BotMessageHandler botMessageHandler)
        {
            var response = await botMessageHandler.HandleIncomingMessageAsync(message);
            return Ok(response);
        }
    }
}
