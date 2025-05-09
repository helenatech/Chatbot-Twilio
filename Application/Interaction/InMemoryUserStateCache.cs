using Chatbot.Application.Interfaces;

namespace Chatbot.Application.Interaction
{
    public class InMemoryUserStateCache : IUserStateCache
    {
        private readonly Dictionary<string, UserState> _cache = new();

        // Método para recuperar o estado do usuário
        public UserState Get(string phoneNumber)
        {
            _cache.TryGetValue(phoneNumber, out var state);
            return state;
        }

        // Método para armazenar o estado do usuário
        public void Set(string phoneNumber, UserState state)
        {
            _cache[phoneNumber] = state;
        }
    }
}
