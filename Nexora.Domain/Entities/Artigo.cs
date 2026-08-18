using System;
using System.Collections.Generic;
using System.Text;

namespace Nexora.Domain.Entities
{
    public class Artigo
    {
        public int Id { get; set; }

        public string Titulo { get; set; } = string.Empty;

        public string Slug { get; set; } = string.Empty;

        public string? Resumo { get; set; } = string.Empty;

        public string Conteudo { get; set; } = string.Empty;

        public string? ImagemUrl { get; set; }

        public bool Publicado { get; set; }

        public DateTime DataPublicacao { get; set; }

        public DateTime DataAtualizacao { get; set; }
    }
}
