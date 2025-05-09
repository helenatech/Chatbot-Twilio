using Chatbot.Application.Interaction;

namespace Chatbot.Application.MessageService.MessagePublicoExternoService
{
    public class InformacoesUteis
    {
        public async Task<string> HandleAsync(UserState userState)
        {
            return userState.CurrentOption switch
            {
                "1" =>
                    "Nossas oportunidades são divulgadas no site do CEUB, pela plataforma da Empregare.\n" +
                    "👉 Acesse e cadastre seu currículo: https://ceub.empregare.com/pt-br\n",
                _ =>
                    "Opção inválida. Digite 0 para voltar ao menu."
            };
        }
    }
}
