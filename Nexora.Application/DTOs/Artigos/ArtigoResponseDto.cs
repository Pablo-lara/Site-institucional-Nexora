namespace Nexora.Application.DTOs.Artigos
{
    public class ArtigoResponseDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Conteudo { get; set; } = string.Empty;
        public string? Resumo { get; set; }
        public string? ImagemUrl { get; set; }
        public bool Publicado { get; set; }
        public DateTime DataPublicacao { get; set; }
    }
}