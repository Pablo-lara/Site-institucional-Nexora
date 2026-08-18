namespace Nexora.Application.DTOs.Empresa
{
    public class EmpresaResponseDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string? Slogan { get; set; }
        public string? SobreNos { get; set; }
        public string? Missao { get; set; }
        public string? Visao { get; set; }
        public string? Valores { get; set; }
        public string? EmailContato { get; set; }
        public string? Telefone { get; set; }
        public string? Endereco { get; set; }
        public string? LinkedinUrl { get; set; }
        public string? InstagramUrl { get; set; }
        public string? GithubUrl { get; set; }
    }
}