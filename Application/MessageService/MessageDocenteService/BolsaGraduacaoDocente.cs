using Chatbot.Application.Interaction;
using Chatbot.Application.Interfaces;
using Chatbot.Domain.Models;

namespace Chatbot.Application.MessageService.MessageDocenteService
{
    public class BolsaGraduacaoDocente : IInteractionService
    {
        public async Task<string> HandleAsync(Client client, UserState userState)
        {
            switch (userState.CurrentOption)
            {
                case "1":
                    return @"
                    Benefício destinado a custear parcialmente a primeira graduação no CEUB do próprio docente ou dos seus dependentes como bolsistas.
                    O custeio se refere especificamente ao valor da matrícula e das mensalidades do curso, não incluindo as disciplinas reprovadas e extracurriculares.
                    Será autorizado o uso de até três bolsas concomitantemente, desde que uma das solicitações seja destinada para o uso próprio do docente, não sendo permitido o uso simultâneo de duas bolsas pelo mesmo beneficiário.
                    É considerado dependente apenas: Cônjuge/companheiro (legal); Filho dependente ou filho do companheiro (a, até completar 24 (vinte e quatro anos , podendo utilizar o benefício até o final do semestre vigente. Pai ou mãe (limitado o desconto de 50%).
                    Para voltar ao menus anterior, digite 0.";
                default:
                    return ("Opção inválida. Digite 0 para voltar ao menu.");
            }
        }
    }
}
