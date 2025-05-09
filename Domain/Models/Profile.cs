using System;
using System.Text.Json.Serialization;

namespace Chatbot.Domain.Models
{
    public class Profile
    {
        public int ProfileId { get; set; }
        public string Name { get; set; }
        public List<Client> Clients { get; set; }

    }
}
