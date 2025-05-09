using Chatbot.Domain.Models;
using Chatbot.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Chatbot.Application.Services
{
    public class AuthenticationService
    {
        private readonly ChatbotDataContext _context;

        public AuthenticationService(ChatbotDataContext context)
        {
            _context = context;
        }

        public async Task<Client> AuthenticateAsync(string clientId, string cpfPrefix)
        {
            var client = await _context
                .Clients
                .Include(c => c.Profile)
                .FirstOrDefaultAsync(c => c.ClientId == clientId && c.CpfPrefix == cpfPrefix);

            if (client == null)
                throw new UnauthorizedAccessException("Matrícula ou CPF inválidos.");

            return client;
        }

    }
}
