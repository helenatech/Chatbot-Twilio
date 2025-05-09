using Chatbot.Application.Interaction;
using Chatbot.Domain.Models;

namespace Chatbot.Application.Interfaces
{
    public interface IInteractionService
    {
        Task<string> HandleAsync(Client client, UserState userState);
    }
}
