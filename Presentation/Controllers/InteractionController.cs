using Chatbot.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Chatbot.Domain.Models;


namespace Chatbot.Presentation.Controllers
{
    [ApiController]
    public class InteractionController : ControllerBase
    {
        [HttpGet("v1/interactions")]
        public async Task<IActionResult> GetAsync(
            [FromServices]ChatbotDataContext context)
        {
            try
            {
                var interactions = await context.Interactions.ToListAsync();
                return Ok(interactions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "05X02 - Falha interna no servidor");
                throw;
            }
        }

        [HttpGet("v1/interactions{id:int}")]
        public async Task<IActionResult> GetByIdAsync(
            [FromRoute] int id,
            [FromServices] ChatbotDataContext context)
        {
            try
            {
                var interaction = await context
                    .Interactions
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (interaction == null)
                    return NotFound();

                return Ok(interaction);
            }
            catch(Exception ex)
            {
                return StatusCode(500, "05X04 - Falha interna no servidor");
            }
        }

        [HttpPost("v1/interactions")]
        public async Task<IActionResult> PostAsync(
            [FromBody] Interaction model,
            [FromServices] ChatbotDataContext context)
        {
            try
            {
                context.Interactions.Add(model);
                await context.SaveChangesAsync();

                return Created($"v1/interactions/{model.Id}", model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "05X08 - Erro ao criar a interação");
            }
        }

        [HttpDelete("v1/interactions/{id:int}")]
        public async Task<IActionResult> DeleteByIdAsync(
            [FromRoute] int id,
            [FromServices] ChatbotDataContext context)
        {
            try
            {
                var interaction = await context
                    .Interactions
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (interaction == null)
                    return NotFound();

                context.Interactions.Remove(interaction);
                await context.SaveChangesAsync();

                return NoContent();

            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "05X07 - Não foi possível deletar a interação");
            }
            catch (Exception e)
            {
                return StatusCode(500, "05X12 - Falha interna no servidor");
                throw;
            }
        }


    }
}
