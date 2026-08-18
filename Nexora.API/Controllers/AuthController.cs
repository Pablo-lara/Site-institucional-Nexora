using Microsoft.AspNetCore.Mvc;
using Nexora.Application.DTOs.Usuarios;
using Nexora.Application.Interfaces.Services;

namespace Nexora.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var resultado = await _authService.AutenticarAsync(dto);
            if (resultado == null)
                return Unauthorized(new { mensagem = "E-mail ou senha inválidos." });

            return Ok(resultado);
        }
    }
}