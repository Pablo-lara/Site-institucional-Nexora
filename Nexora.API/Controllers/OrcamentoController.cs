using Microsoft.AspNetCore.Mvc;
using Nexora.Application.DTOs.Orcamentos;
using Nexora.Application.Interfaces.Services;

namespace Nexora.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrcamentoController : ControllerBase
    {
        private readonly IOrcamentoService _orcamentoService;

        public OrcamentoController(IOrcamentoService orcamentoService)
        {
            _orcamentoService = orcamentoService;
        }

        // Endpoint público: solicitação de orçamento pelo visitante
        [HttpPost]
        public async Task<IActionResult> Solicitar([FromBody] SolicitarOrcamentoDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var novoOrcamento = await _orcamentoService.SolicitarAsync(dto);
            return CreatedAtAction(nameof(ObterPorId), new { id = novoOrcamento.Id }, novoOrcamento);
        }

        // Endpoints administrativos
        [HttpGet("admin/todos")]
        public async Task<IActionResult> ObterTodos()
        {
            var orcamentos = await _orcamentoService.ObterTodosAsync();
            return Ok(orcamentos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> ObterPorId(int id)
        {
            var orcamento = await _orcamentoService.ObterPorIdAsync(id);
            if (orcamento == null)
                return NotFound(new { mensagem = "Solicitação de orçamento não encontrada." });

            return Ok(orcamento);
        }

        [HttpPatch("{id:int}/status")]
        public async Task<IActionResult> AtualizarStatus(int id, [FromBody] AtualizarStatusOrcamentoDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var sucesso = await _orcamentoService.AtualizarStatusAsync(id, dto);
            if (!sucesso)
                return NotFound(new { mensagem = "Solicitação de orçamento não encontrada." });

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Deletar(int id)
        {
            var deletado = await _orcamentoService.DeletarAsync(id);
            if (!deletado)
                return NotFound(new { mensagem = "Solicitação de orçamento não encontrada." });

            return NoContent();
        }
    }
}