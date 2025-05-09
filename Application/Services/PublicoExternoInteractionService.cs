using Chatbot.Application.Interaction;
using Chatbot.Application.MessageService.MessagePublicoExternoService;
using System.Text;

namespace Chatbot.Application.Services
{
    public class PublicoExternoInteractionService
    {
        private readonly InformacoesUteis _informacoesUteis;
        public PublicoExternoInteractionService(InformacoesUteis informacoesUteis)
        {
            _informacoesUteis = informacoesUteis;
        }

        public async Task<string> HandleAsync(UserState userState)
        {
            if (string.IsNullOrEmpty(userState.CurrentOption))
            {
                return await ShowInitialOptions();
            }
            else
            {
                return await HandleSelectedOption(userState);
            }
        }
        public async Task<string> HandleSelectedOption(UserState userState)
        {
            switch (userState.CurrentOption)
            {
                case "1":
                    return await _informacoesUteis.HandleAsync(userState);
                default:
                    return "Opção inválida.";
            }
        }
        public async Task<string> ShowInitialOptions()
        {
            var options = new StringBuilder();

            options.AppendLine($"Olá, bem-vindo(a) ao atendimento do RH.");
            options.AppendLine("1. Processo Seletivo");

            return await Task.FromResult(options.ToString());
           
        }
    }
}