using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nexora.Application.DTOs.Artigos;
using Nexora.Application.Interfaces.Services;

namespace Nexora.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArtigoController : ControllerBase
    {
        private readonly IArtigoService _artigoService;

        public ArtigoController(IArtigoService artigoService)
        {
            _artigoService = artigoService;
        }

        [HttpGet]
        public async Task<IActionResult> ObterPublicados()
        {
            var artigos = await _artigoService.ObterPublicadosAsync();
            return Ok(artigos);
        }

        [HttpGet("admin/todos")]
        [Authorize]
        public async Task<IActionResult> ObterTodos()
        {
            var artigos = await _artigoService.ObterTodosAsync();
            return Ok(artigos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> ObterPorId(int id)
        {
            var artigo = await _artigoService.ObterPorIdAsync(id);
            if (artigo == null)
                return NotFound(new { mensagem = "Artigo não encontrado." });

            return Ok(artigo);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Criar([FromBody] CriarArtigoDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var novoArtigo = await _artigoService.CriarAsync(dto);
            return CreatedAtAction(nameof(ObterPorId), new { id = novoArtigo.Id }, novoArtigo);
        }

        [HttpPut("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Atualizar(int id, [FromBody] AtualizarArtigoDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var atualizado = await _artigoService.AtualizarAsync(id, dto);
            if (!atualizado)
                return NotFound(new { mensagem = "Artigo não encontrado." });

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Deletar(int id)
        {
            var deletado = await _artigoService.DeletarAsync(id);
            if (!deletado)
                return NotFound(new { mensagem = "Artigo não encontrado." });

            return NoContent();
        }
    }
}