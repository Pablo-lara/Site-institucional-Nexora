namespace Nexora.Application.DTOs.Projetos;

public class ProjetoResponseDto
{
    public int Id { get; set; }

    public string Nome { get; set; } = string.Empty;

    public string Descricao { get; set; } = string.Empty;

    public string? Cliente {  get; set; } = string.Empty;

    public string? ImagemUrl { get; set; }

    public string? UrlProjeto { get; set; }

    public string? Tecnologias { get; set; }

    public bool Destaque { get; set; }

    public bool Ativo { get; set; }

    public DateTime DataCriacao { get; set; }

    public DateTime? DataAtualizacao { get; set; }
}