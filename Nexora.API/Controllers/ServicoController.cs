using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nexora.Application.DTOs.Servicos;
using Nexora.Application.Interfaces.Services;

namespace Nexora.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ServicoController : ControllerBase
    {
        private readonly IServicoService _service;

        public ServicoController(IServicoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServicoResponseDto>>> ObterTodos()
        {
            var servicos = await _service.ObterTodosAsync();

            return Ok(servicos);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ServicoResponseDto>> ObterPorId(int id)
        {
            var servico = await _service.ObterPorIdAsync(id);

            if (servico is null)
                return NotFound();

            return Ok(servico);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ServicoResponseDto>> Criar(
            [FromBody] CriarServicoDto dto)
        {
            var servico = await _service.CriarAsync(dto);

            return CreatedAtAction(
                nameof(ObterPorId),
                new { id = servico.Id },
                servico);
        }

        [HttpPut("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Atualizar(
            int id,
            AtualizarServicoDto dto)
        {
            var atualizado = await _service.AtualizarAsync(id, dto);

            if (!atualizado)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Remover(int id)
        {
            var removido = await _service.RemoverAsync(id);

            if (!removido)
                return NotFound();

            return NoContent();
        }
    }
}
