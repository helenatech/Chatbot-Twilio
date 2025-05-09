using Chatbot.Application.Interaction;
using Chatbot.Application.Interfaces;
using Chatbot.Domain.Models;

namespace Chatbot.Application.MessageService.MessageAdministrativoService
{
    public class BolsaGraduacaoAdm : IInteractionService
    {
        public async Task<string> HandleAsync(Client client, UserState userState)
        {
            switch (userState.CurrentOption)
            {
                case "1":
                    return @"                
                    Benefício destinado a .......";
                default:
                    return ("Opção inválida! Digite 0 para voltar ao menu.");
            }               
        }
    }
}
