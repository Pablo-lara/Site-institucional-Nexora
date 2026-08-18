using System.ComponentModel.DataAnnotations;
using Nexora.Domain.Enums;

namespace Nexora.Application.DTOs.Orcamentos
{
    public class AtualizarStatusOrcamentoDto
    {
        [Required(ErrorMessage = "O status é obrigatório.")]
        public StatusOrcamento Status { get; set; }

        [StringLength(1000, ErrorMessage = "As observações devem ter no máximo 1000 caracteres.")]
        public string? ObservacoesAdmin { get; set; }
    }
}