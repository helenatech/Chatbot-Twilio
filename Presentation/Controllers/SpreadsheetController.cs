using Chatbot.Infrastructure.Importers;
using Microsoft.AspNetCore.Mvc;

namespace Chatbot.Presentation.Controllers
{
    [ApiController]
    public class SpreadsheetController : ControllerBase
    {
        private readonly SpreadsheetImporter _spreadsheetImporter;

        public SpreadsheetController(SpreadsheetImporter spreadsheetImporter)
        {
            _spreadsheetImporter = spreadsheetImporter;
        }

        [HttpPost("import-clients")]
        public async Task<IActionResult> ImportClients()
        {
            try
            {
                await _spreadsheetImporter.ImportClientsAsync();
                return Ok("Usuários importados com sucesso!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao importar usuários: {ex.Message}");
            }
        }
    }
}
