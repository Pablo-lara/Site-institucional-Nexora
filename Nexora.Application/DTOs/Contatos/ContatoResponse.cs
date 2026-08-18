namespace Nexora.Application.DTOs.Contatos
{
    public class ContatoResponseDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Telefone { get; set; }
        public string? Assunto { get; set; }
        public string Mensagem { get; set; } = string.Empty;
        public bool Lida { get; set; }
        public DateTime DataEnvio { get; set; }
    }
}