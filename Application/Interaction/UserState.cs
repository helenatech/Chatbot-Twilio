namespace Chatbot.Application.Interaction
{
    public class UserState
    {
        public string? CurrentOption { get; set; }
        public string? CurrentStep { get; set; }
        public string? Profile { get; set; }
        public bool IsAuthenticated{ get; set; }
        public string? ClientId { get; set; }
        public string? CpfPrefix { get; set; }
    }
}
