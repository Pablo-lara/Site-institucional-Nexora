using Nexora.Domain.Enums;

namespace Nexora.Application.DTOs.Orcamentos
{
    public class OrcamentoResponseDto
    {
        public int Id { get; set; }
        public string NomeCliente { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        //public string? NomeEmpresa { get; set; }
        public int? ServicoId { get; set; }
        public string? NomeServico { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public StatusOrcamento Status { get; set; }
        public string StatusDescricao => Status.ToString();
        public string? ObservacoesAdmin { get; set; }
        public DateTime DataSolicitacao { get; set; }
    }
}