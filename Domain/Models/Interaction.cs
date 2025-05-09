namespace Chatbot.Domain.Models
{
    public class Interaction
    {
        public int Id { get; set; }                  
        public Client? Client { get; set; }  
        public Avaliation? Avaliation { get; set; }
        public string? ClientId { get; set; }
        public required string ClientMessage { get; set; }
        public string? Intention { get; set; }      
        public string? BotMessage { get; set; }      
        public DateTime CreatedAt { get; set; }      
        public Guid SessionId { get; set; }          

    }
}
