
namespace Chatbot.Domain.Models
{
    public class Client
    {
        public string ClientId { get; set; } = string.Empty;
        public string Name { get; set; }
        public string CpfPrefix { get; set; }      
        public string Email { get; set; }
        public string Status { get; set; }
        public DateTime LastUpdatedAt { get; set; }

        public int ProfileId { get; set; }
        public Profile Profile { get; set; }  
        public List<Interaction> Interactions { get; set; }
    }
}
