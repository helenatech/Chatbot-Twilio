using Chatbot.Domain.Models;
using Chatbot.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Chatbot.Presentation.Controllers
{
    [ApiController]
    public class AvaliationController : ControllerBase
    {

        [HttpGet("v1/avaliations")]
        public async Task<IActionResult> GetAsync(
            [FromServices] ChatbotDataContext context)
        {
            try
            {
                var avaliations = await context.Avaliations.ToListAsync();
                return Ok(avaliations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "05X10 - Falha ao buscar avaliações");
            }
        }

        [HttpGet("v1/avaliations/{id:int}")]
        public async Task<IActionResult> GetByIdAsync(
            [FromRoute] int id,
            [FromServices] ChatbotDataContext context)
        {
            try
            {
                var avaliation = await context.Avaliations
                    .FirstOrDefaultAsync(e => e.Id == id);

                if (avaliation == null)
                    return NotFound();

                return Ok(avaliation);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "05X11 - Falha interna no servidor");
            }
        }

        [HttpPost("v1/avaliations")]
        public async Task<IActionResult> PostAsync(
            [FromBody] Avaliation model,
            [FromServices] ChatbotDataContext context)
        {
            try
            {
                context.Avaliations.Add(model);
                await context.SaveChangesAsync();

                return Created($"v1/avaliations/{model.Id}", model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "05X09 - Erro ao criar a avaliação");
            }
        }
        [HttpDelete("v1/avaliations/{id:int}")]
        public async Task<IActionResult> DeleteByIdAsync(
            [FromRoute] int id,
            [FromServices] ChatbotDataContext context)
        {
            try
            {
                var avaliation = await context.Avaliations
                    .FirstOrDefaultAsync(e => e.Id == id);

                if (avaliation == null)
                    return NotFound();

                context.Avaliations.Remove(avaliation);
                await context.SaveChangesAsync();

                return NoContent();
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, "05X12 - Erro ao deletar a avaliação");
            }
            catch (Exception)
            {
                return StatusCode(500, "05X13 - Falha interna no servidor");
            }
        }
    }
}

