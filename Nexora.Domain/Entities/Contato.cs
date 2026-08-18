using System;
using System.Collections.Generic;
using System.Text;

namespace Nexora.Domain.Entities
{
    public class Contato
    {
        public int Id { get; set; }

        public string Nome { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string? Telefone { get; set; }

        public string Assunto { get; set; } = string.Empty;

        public string Mensagem { get; set; } = string.Empty;

        public bool Lido { get; set; }

        public DateTime DataEnvio { get; set; }
    }
}
