using Chatbot.Application.DTOs;
using Chatbot.Domain.Models;
using Chatbot.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Chatbot.Presentation.Controllers
{
    [ApiController]
    public class ClientController : ControllerBase
    {
        [HttpGet("v1/clients")]
        public async Task<IActionResult> GetClientsAsync(
            [FromServices] ChatbotDataContext context,
            [FromQuery] int page = 0,
            [FromQuery] int pageSize = 25)
        {
            try
            {
                var clients = await context.Clients
                    .AsNoTracking()
                    .Include(x => x.Profile)
                    .Select(x => new ClientDto
                    {
                        ClientId = x.ClientId,
                        Name = x.Name,
                        CpfPrefix = x.CpfPrefix,
                        Email = x.Email,
                        Status = x.Status,
                        ProfileName = x.Profile.Name
                    })
                    .Skip(page * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return Ok(clients);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "05X11 - Falha ao buscar clientes");
            }
        }

        [HttpGet("v1/clients/{id:int}")]
        public async Task<IActionResult> GetByIdAsync(
            [FromRoute] string id,
            [FromServices] ChatbotDataContext context)
        {
            try
            {
                var client = await context
                    .Clients
                    .AsNoTracking()
                    .Select(x => new ClientDto
                    {
                        ClientId = x.ClientId,
                        Name = x.Name,
                        CpfPrefix = x.CpfPrefix,
                        Email = x.Email,
                        Status = x.Status,
                        ProfileName = x.Profile.Name
                     })
                    .FirstOrDefaultAsync(x => x.ClientId == id);

                if (client == null)
                    return NotFound("Usuário não encontrado");

                return Ok(client);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "05X15 - Falha ao buscar cliente");
            }
        }

        [HttpPost("v1/clients")]
        public async Task<IActionResult> PostAsync(
            [FromBody] Client model,
            [FromServices] ChatbotDataContext context)
        {
            try
            {
                await context.Clients.AddAsync(model);
                await context.SaveChangesAsync();

                return Created($"v1/clients/{model.ClientId}", model);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "05XE2 - Não foi possível cadastar o cliente");
            }
            catch (Exception e)
            {
                return StatusCode(500, "05XE5 - Falha interna no servidor");
            }
        }

        [HttpDelete("v1/cllients")]
        public async Task<IActionResult> DeleteAsync(
            [FromRoute] string id,
            [FromServices] ChatbotDataContext context)
        {
            try
            {
                var client = await context.Clients.FirstOrDefaultAsync(x => x.ClientId == id);

                if (client == null)
                    return NotFound();

                context.Clients.Remove(client);
                await context.SaveChangesAsync();

                return NoContent();
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "05XE7 - Não foi possível cadastar o cliente");
            }
            catch (Exception e)
            {
                return StatusCode(500, "05XE18 - Falha interna no servidor");
            }
        }

    }
}
