using System;
using System.Collections.Generic;
using System.Text;
using Nexora.Domain.Enums;

namespace Nexora.Domain.Entities
{
    public class Orcamento
    {
        public int Id { get; set; }

        public string NomeCliente { get; set; } = string.Empty;

        public string NomeEmpresa { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string? Telefone { get; set; }

        public virtual Servico? Servico { get; set; }

        public int? ServicoId { get; set; }

        public string Descricao { get; set; } = string.Empty;

        public string ObservacoesAdmin {  get; set; } = string.Empty;

        public string? FaixaOrcamento { get; set; }

        public StatusOrcamento Status { get; set; }

        public DateTime DataSolicitacao { get; set; }
    }
}
