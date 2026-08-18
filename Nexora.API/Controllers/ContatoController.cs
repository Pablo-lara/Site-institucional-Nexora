using Microsoft.AspNetCore.Mvc;
using Nexora.Application.DTOs.Contatos;
using Nexora.Application.Interfaces.Services;

namespace Nexora.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContatoController : ControllerBase
    {
        private readonly IContatoService _contatoService;

        public ContatoController(IContatoService contatoService)
        {
            _contatoService = contatoService;
        }

        // Endpoint público: envio do formulário de contato pelo visitante
        [HttpPost]
        public async Task<IActionResult> Enviar([FromBody] EnviarContatoDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var novoContato = await _contatoService.EnviarAsync(dto);
            return CreatedAtAction(nameof(ObterPorId), new { id = novoContato.Id }, novoContato);
        }

        // Endpoints administrativos
        [HttpGet("admin/todos")]
        public async Task<IActionResult> ObterTodos()
        {
            var contatos = await _contatoService.ObterTodosAsync();
            return Ok(contatos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> ObterPorId(int id)
        {
            var contato = await _contatoService.ObterPorIdAsync(id);
            if (contato == null)
                return NotFound(new { mensagem = "Mensagem de contato não encontrada." });

            return Ok(contato);
        }

        [HttpPatch("{id:int}/marcar-lida")]
        public async Task<IActionResult> MarcarComoLida(int id)
        {
            var sucesso = await _contatoService.MarcarComoLidaAsync(id);
            if (!sucesso)
                return NotFound(new { mensagem = "Mensagem de contato não encontrada." });

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Deletar(int id)
        {
            var deletado = await _contatoService.DeletarAsync(id);
            if (!deletado)
                return NotFound(new { mensagem = "Mensagem de contato não encontrada." });

            return NoContent();
        }
    }
}