using Chatbot.Application.Interaction;
using Chatbot.Application.Interfaces;
using Chatbot.Application.MessageService;
using Chatbot.Application.MessageService.MessageDocenteService;
using Chatbot.Domain.Models;
using System.Text;

namespace Chatbot.Application.Services
{
    public class DocenteInteractionService : IInteractionService
    {
        private readonly BolsaGraduacaoDocente _bolsaGraduacaoService;
        public DocenteInteractionService(BolsaGraduacaoDocente bolsaGraduacaoService)
        {
            _bolsaGraduacaoService = bolsaGraduacaoService;
        }

        public async Task<string> HandleAsync(Client client, UserState userState)
        {
            if (string.IsNullOrEmpty(userState.CurrentOption))
            {
                return await ShowInitialOptions(client);
            }
            else
            {
                return await HandleSelectedOption(client, userState);
            }
        }


        private async Task<string> HandleSelectedOption(Client client, UserState userState)
        {
            switch (userState.CurrentOption)
            {
                case "1":
                    return await _bolsaGraduacaoService.HandleAsync(client, userState);
                default:
                    return "Opção inválida.";
            }
        }
        public async Task<string> ShowInitialOptions(Client client)
        {
            var options = new StringBuilder();

            options.AppendLine($"Olá {client.Name}, bem-vindo(a) ao atendimento de Docente! Veja as opções abaixo:");
            options.AppendLine("1. Bolsa Graduação");
            options.AppendLine("2. Bolsa Pós Graduação");
            options.AppendLine("3. Contracheque e informe de rendimentos");
            options.AppendLine("4. E Mail institucional");
            options.AppendLine("5. SGI");
            options.AppendLine("6. Atestados e Licenças");
            options.AppendLine("7. Plano de Carreira Docentes");
            options.AppendLine("8. Crachá");
            options.AppendLine("9. Benefícios");
            options.AppendLine("10. Rescisão, desligamento e homologação");
            options.AppendLine("11. Calendário Acadêmico");

            return await Task.FromResult(options.ToString());
        }
    }
}

