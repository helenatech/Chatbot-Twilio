using Chatbot.Domain.Models;
using Chatbot.Application.Interfaces;
using Chatbot.Application.Services;

namespace Chatbot.Application.Interaction
{
    public class UserInteractionRouter 
    {
        private readonly DocenteInteractionService _docenteService;
        private readonly AdministrativoInteractionService _administrativoService;
        public UserInteractionRouter(
            DocenteInteractionService docenteService,
            AdministrativoInteractionService administrativoService)
        {
            _docenteService = docenteService;
            _administrativoService = administrativoService;
        }

        public async Task<string> HandleInteractionAsync(Client client, UserState userState)
        {
            var profileName = client.Profile?.Name?.Trim();

            return profileName switch
            {
                "Docentes" => await _docenteService.HandleAsync(client, userState),
                "Administrativo" => await _administrativoService.HandleAsync(client, userState),
                _ => "Perfil desconhecido. Entre em contato com o suporte."
            };
        }
    }
}
