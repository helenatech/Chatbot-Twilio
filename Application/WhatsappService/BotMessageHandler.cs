using Chatbot.Application.DTOs;
using Chatbot.Application.Interaction;
using Chatbot.Application.Interfaces;
using Chatbot.Application.Services;
using Chatbot.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Chatbot.Application.WhatsappService
{
    public class BotMessageHandler
    {
        private readonly ChatbotDataContext _context;
        private readonly UserInteractionRouter _interactionRouter;
        private readonly TwilioMessageSender _messageSender;
        private readonly IUserStateCache _stateCache;
        private readonly AuthenticationService _authenticationService;
        private readonly PublicoExternoInteractionService _externoService;

        public BotMessageHandler(
            ChatbotDataContext context,
            UserInteractionRouter interactionRouter,
            TwilioMessageSender messageSender,
            IUserStateCache stateCache,
            AuthenticationService authenticationService,
            PublicoExternoInteractionService externoService)
        {
            _context = context;
            _interactionRouter = interactionRouter;
            _messageSender = messageSender;
            _stateCache = stateCache;
            _authenticationService = authenticationService;
            _externoService = externoService;
        }

        public async Task<string> HandleIncomingMessageAsync(TwilioMessageDto message)
        {
            var userState = _stateCache.Get(message.From) ?? new UserState();
            var input = message.Body.Trim();


            if (string.IsNullOrEmpty(userState.Profile))
            {
                if (input.Equals("Publico Externo", StringComparison.OrdinalIgnoreCase))
                {
                    userState.Profile = "Publico Externo";
                    userState.IsAuthenticated = true;
                    _stateCache.Set(message.From, userState);
                    return await _externoService.ShowInitialOptions();
                }

                if (input.Equals("Docentes", StringComparison.OrdinalIgnoreCase) || input.Equals("Administrativo", StringComparison.OrdinalIgnoreCase))
                {
                    userState.Profile = input.Trim();
                    userState.CurrentStep = "AwaitingMatricula";
                    _stateCache.Set(message.From, userState);
                    return "Informe sua matrícula:";
                }

                return "Olá! Você é Docente, Administrativo ou Público Externo?";
            }

            if (userState.CurrentStep == "AwaitingMatricula")
            {
                userState.ClientId = input.Trim();
                userState.CurrentStep = "AwaitingCpf";
                _stateCache.Set(message.From, userState);
                return "Informe os 4 primeiros dígitos do seu CPF:";
            }

            if (!userState.IsAuthenticated && (userState.Profile == "Docentes" || userState.Profile == "Administrativo"))
            {
                try
                {
                    var client = await _authenticationService.AuthenticateAsync(userState.ClientId, input);
                    userState.IsAuthenticated = true;
                    userState.Profile = client.Profile?.Name?.Trim().ToLowerInvariant();
                    Console.WriteLine($"[DEBUG] {userState.Profile} checando o perfil aq");
                    _stateCache.Set(message.From, userState);


                    return await _interactionRouter.HandleInteractionAsync(client, userState);
                }
                catch
                {
                    return "Matrícula ou CPF inválidos. Tente novamente.";
                }
            }

            if (userState.Profile == "Público Externo")
            {
                return await _externoService.HandleAsync(userState);
            }

            var clientData = await _context
                .Clients
                .Include(c => c.Profile)
                .FirstOrDefaultAsync(c => c.ClientId == userState.ClientId);

            if (clientData == null)
            {
                return "Usuário não encontrado no sistema.";
            }

            if (userState.IsAuthenticated && string.IsNullOrEmpty(userState.CurrentOption) && int.TryParse(input, out _))
            {
                userState.CurrentOption = input;
                _stateCache.Set(message.From, userState);
            }

            return await _interactionRouter.HandleInteractionAsync(clientData, userState);
        }
    }
}

