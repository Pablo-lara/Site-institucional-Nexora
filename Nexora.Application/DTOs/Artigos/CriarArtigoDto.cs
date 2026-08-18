using System.ComponentModel.DataAnnotations;

namespace Nexora.Application.DTOs.Artigos
{
    public class CriarArtigoDto
    {
        [Required(ErrorMessage = "O título é obrigatório.")]
        [StringLength(150, ErrorMessage = "O título deve ter no máximo 150 caracteres.")]
        public string Titulo { get; set; } = string.Empty;

        [Required(ErrorMessage = "O conteúdo é obrigatório.")]
        public string Conteudo { get; set; } = string.Empty;

        [StringLength(300, ErrorMessage = "O resumo deve ter no máximo 300 caracteres.")]
        public string? Resumo { get; set; }

        public string? ImagemUrl { get; set; }
        public bool Publicado { get; set; } = true;
    }
}