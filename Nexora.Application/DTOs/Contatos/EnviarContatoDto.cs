using System.ComponentModel.DataAnnotations;

namespace Nexora.Application.DTOs.Contatos
{
    public class EnviarContatoDto
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "Informe um e-mail válido.")]
        [StringLength(150, ErrorMessage = "O e-mail deve ter no máximo 150 caracteres.")]
        public string Email { get; set; } = string.Empty;

        [Phone(ErrorMessage = "Informe um número de telefone válido.")]
        [StringLength(20, ErrorMessage = "O telefone deve ter no máximo 20 caracteres.")]
        public string? Telefone { get; set; }

        [StringLength(150, ErrorMessage = "O assunto deve ter no máximo 150 caracteres.")]
        public string? Assunto { get; set; }

        [Required(ErrorMessage = "A mensagem é obrigatória.")]
        [StringLength(2000, ErrorMessage = "A mensagem deve ter no máximo 2000 caracteres.")]
        public string Mensagem { get; set; } = string.Empty;
    }
}