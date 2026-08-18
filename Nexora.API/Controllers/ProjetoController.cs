using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nexora.Application.DTOs.Projetos;
using Nexora.Application.Interfaces.Services;

namespace Nexora.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjetoController : ControllerBase
    {
        private readonly IProjetoService _projetoService;

        public ProjetoController(IProjetoService projetoService)
        {
            _projetoService = projetoService;
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodos()
        {
            var projetos = await _projetoService.ObterTodosAsync();
            return Ok(projetos);
        }

        [HttpGet("destaques")]
        public async Task<IActionResult> ObterDestaques()
        {
            var destaques = await _projetoService.ObterDestaquesAsync();
            return Ok(destaques);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> ObterPorId(int id)
        {
            var projeto = await _projetoService.ObterPorIdAsync(id);
            if (projeto == null)
                return NotFound(new { mensagem = "Projeto não encontrado." });

            return Ok(projeto);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Criar([FromBody] CriarProjetoDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var novoProjeto = await _projetoService.CriarAsync(dto);
            return CreatedAtAction(nameof(ObterPorId), new { id = novoProjeto.Id }, novoProjeto);
        }

        [HttpPut("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Atualizar(int id, [FromBody] AtualizarProjetoDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var atualizado = await _projetoService.AtualizarAsync(id, dto);
            if (!atualizado)
                return NotFound(new { mensagem = "Projeto não encontrado para atualização." });

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Deletar(int id)
        {
            var deletado = await _projetoService.RemoverAsync(id);
            if (!deletado)
                return NotFound(new { mensagem = "Projeto não encontrado para remoção." });

            return NoContent();
        }
    }
}