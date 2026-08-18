using System;
using System.Collections.Generic;
using System.Text;

namespace Nexora.Domain.Entities
{
    public class Empresa
    {
        public int Id { get; set; }

        public string Nome { get; set; } = string.Empty;

        public string Descricao { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string? Telefone { get; set; }

        public string? Endereco { get; set; }

        public string? Instagram { get; set; }

        public string? Linkedin { get; set; }

        public string? Github { get; set; }

        public string? LogoUrl { get; set; }
    }
}
