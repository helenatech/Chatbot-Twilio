using Chatbot.Domain.Models;
using Chatbot.Infrastructure.Data;
using Chatbot.Infrastructure.Services;
using Chatbot.Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Chatbot.Infrastructure.Importers
{
    public class SpreadsheetImporter
    {
        private readonly ExcelReaderService _excelReader;
        private readonly ChatbotDataContext _context;
        private readonly SpreadsheetSettings _settings;

        public SpreadsheetImporter(ExcelReaderService excelReader, ChatbotDataContext context, IOptions<SpreadsheetSettings> options)
        {
            _excelReader = excelReader;
            _context = context;
            _settings = options.Value;
        }

        public async Task ImportClientsAsync()
        {
            _context.Clients.RemoveRange(_context.Clients); //vai limpar a tabela antes de importar tudo de novo 
            await _context.SaveChangesAsync();

            var filePath = _settings.ClientFilePath;

            var importedClients = await _excelReader.ReadDataFromFileAsync<ClientImportModel>(_settings.ClientFilePath);

            var groupedClients = importedClients
                .GroupBy(x => new { x.CpfPrefix, x.Email, x.Name }) // ajusta conforme necessário
                .ToList();

            var allProfiles = await _context.Profiles.ToListAsync();

            foreach (var group in groupedClients)
            {
                var first = group.First();

                var profileName = first.Profile?.Trim().ToLower();

                if (string.IsNullOrWhiteSpace(profileName))
                {
                    Console.WriteLine($"[AVISO] Perfil não informado para o client: {first.Name} ({first.ClientId})");
                }

                var profile = allProfiles
                    .FirstOrDefault(p => p.Name.ToLower() == profileName);

                if (profile == null)
                {
                    Console.WriteLine($"[ERRO] Perfil '{first.Profile}' não encontrado para cliente '{first.Name}'");
                    continue;
                }

                var client = new Client
                {
                    ClientId = first.ClientId,
                    Name = first.Name,
                    CpfPrefix = first.CpfPrefix,
                    Email = first.Email,
                    Status = first.Status,
                    LastUpdatedAt = DateTime.UtcNow,
                    Profile = profile
                };

                _context.Clients.Add(client);
            }

            await _context.SaveChangesAsync();
            Console.WriteLine("Importações finalizadas.");
        }
    }
}
