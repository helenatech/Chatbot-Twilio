using Chatbot.Application.Interaction;

namespace Chatbot.Application.Interfaces
{
    public interface IUserStateCache
    {
        // Recupera o estado do usuário com base no número de telefone
        UserState? Get(string phoneNumber);

        // Armazena o estado do usuário com base no número de telefone
        void Set(string phoneNumber, UserState state);
    }
}
