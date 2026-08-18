using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Nexora.Application.DTOs.Servicos
{
    public class CriarServicoDto
    {

        [Required(ErrorMessage = "O nome do serviço é obrigatório.")]
        [StringLength(100, MinimumLength = 3,
            ErrorMessage = "O nome deve possuir entre 3 e 100 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "A descrição do serviço é obrigatória.")]
        [StringLength(500,
            ErrorMessage = "A descrição deve possuir no máximo 500 caracteres.")]
        public string Descricao { get; set; } = string.Empty;

        [Range(0, double.MaxValue,
            ErrorMessage = "O preço não pode ser negativo.")]
        public decimal Preco { get; set; }

        [Url(ErrorMessage = "A URL da imagem informada é inválida.")]
        public string? ImagemUrl { get; set; }

        public bool Ativo { get; set; } = true;
    }
}
