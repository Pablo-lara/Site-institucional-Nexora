using System.ComponentModel.DataAnnotations;

namespace Nexora.Application.DTOs.Empresa
{
    public class AtualizarEmpresaDto
    {
        [Required(ErrorMessage = "O nome da empresa é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        [StringLength(200, ErrorMessage = "O slogan deve ter no máximo 200 caracteres.")]
        public string? Slogan { get; set; }

        public string? SobreNos { get; set; }
        public string? Missao { get; set; }
        public string? Visao { get; set; }
        public string? Valores { get; set; }

        [EmailAddress(ErrorMessage = "E-mail inválido.")]
        public string? EmailContato { get; set; }

        public string? Telefone { get; set; }
        public string? Endereco { get; set; }

        public string? LinkedinUrl { get; set; }
        public string? InstagramUrl { get; set; }
        public string? GithubUrl { get; set; }
    }
}