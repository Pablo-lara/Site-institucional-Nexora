using System;
using System.Collections.Generic;
using System.Text;
using Nexora.Domain.Enums;

namespace Nexora.Domain.Entities
{
    public class Usuario
    {
        public int Id { get; set; }

        public string Nome { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string SenhaHash { get; set; } = string.Empty;

        public PerfilUsuario Perfil { get; set; }

        public DateTime DataCriacao { get; set; }
    }
}
