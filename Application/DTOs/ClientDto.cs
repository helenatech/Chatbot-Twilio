using Chatbot.Domain.Models;

namespace Chatbot.Application.DTOs
{
    public class ClientDto
    {
        public string ClientId { get; set; }
        public string Name { get; set; }
        public string CpfPrefix { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public string ProfileName { get; set; }
    }

}
