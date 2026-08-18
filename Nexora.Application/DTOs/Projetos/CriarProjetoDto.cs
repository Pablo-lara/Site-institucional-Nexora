using System.ComponentModel.DataAnnotations;

namespace Nexora.Application.DTOs.Projetos;

public class CriarProjetoDto
{
    [Required(ErrorMessage = "O nome do projeto é obrigatório.")]
    [StringLength(
        150,
        MinimumLength = 3,
        ErrorMessage = "O nome deve possuir entre 3 e 150 caracteres.")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "A descrição do projeto é obrigatória.")]
    [StringLength(
        1000,
        MinimumLength = 10,
        ErrorMessage = "A descrição deve possuir entre 10 e 1000 caracteres.")]
    public string Descricao { get; set; } = string.Empty;

    [Url(ErrorMessage = "A URL da imagem informada é inválida.")]
    public string? ImagemUrl { get; set; }

    [Url(ErrorMessage = "A URL do projeto informada é inválida.")]
    public string? UrlProjeto { get; set; }

    [StringLength(
        500,
        ErrorMessage = "As tecnologias devem possuir no máximo 500 caracteres.")]
    public string? Tecnologias { get; set; }

    public bool Destaque { get; set; }

    public bool Ativo { get; set; } = true;

    public string? Cliente { get; set; } = string.Empty;
}