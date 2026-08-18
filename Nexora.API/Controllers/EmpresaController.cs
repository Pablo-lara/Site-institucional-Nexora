using Microsoft.AspNetCore.Mvc;
using Nexora.Application.DTOs.Empresa;
using Nexora.Application.Interfaces.Services;

namespace Nexora.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpresaController : ControllerBase
    {
        private readonly IEmpresaService _empresaService;

        public EmpresaController(IEmpresaService empresaService)
        {
            _empresaService = empresaService;
        }

        // Endpoint público para exibir na Home, Sobre e Rodapé
        [HttpGet]
        public async Task<IActionResult> ObterInformacoes()
        {
            var empresa = await _empresaService.ObterInformacoesAsync();
            if (empresa == null)
                return NotFound(new { mensagem = "Informações institucionais ainda não foram cadastradas." });

            return Ok(empresa);
        }

        // Endpoint administrativo para atualizar/cadastrar os dados institucionais
        [HttpPut("admin")]
        public async Task<IActionResult> SalvarInformacoes([FromBody] AtualizarEmpresaDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var empresaAtualizada = await _empresaService.SalvarInformacoesAsync(dto);
            return Ok(empresaAtualizada);
        }
    }
}