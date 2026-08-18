using Nexora.Application.DTOs.Artigos;
using Nexora.Application.Interfaces.Repositories;
using Nexora.Application.Interfaces.Services;
using Nexora.Domain.Entities;

namespace Nexora.Application.Services
{
    public class ArtigoService : IArtigoService
    {
        private readonly IArtigoRepository _artigoRepository;

        public ArtigoService(IArtigoRepository artigoRepository)
        {
            _artigoRepository = artigoRepository;
        }

        public async Task<IEnumerable<ArtigoResponseDto>> ObterTodosAsync()
        {
            var artigos = await _artigoRepository.ObterTodosAsync();
            return artigos.Select(MapearParaDto);
        }

        public async Task<IEnumerable<ArtigoResponseDto>> ObterPublicadosAsync()
        {
            var artigos = await _artigoRepository.ObterPublicadosAsync();
            return artigos.Select(MapearParaDto);
        }

        public async Task<ArtigoResponseDto?> ObterPorIdAsync(int id)
        {
            var artigo = await _artigoRepository.ObterPorIdAsync(id);
            return artigo == null ? null : MapearParaDto(artigo);
        }

        public async Task<ArtigoResponseDto> CriarAsync(CriarArtigoDto dto)
        {
            var artigo = new Artigo
            {
                Titulo = dto.Titulo,
                Conteudo = dto.Conteudo,
                Resumo = dto.Resumo,
                ImagemUrl = dto.ImagemUrl,
                Publicado = dto.Publicado,
                Slug = GerarSlug(dto.Titulo),
                DataPublicacao = DateTime.UtcNow
            };

            var criado = await _artigoRepository.CriarAsync(artigo);
            return MapearParaDto(criado);
        }

        public async Task<bool> AtualizarAsync(int id, AtualizarArtigoDto dto)
        {
            var artigo = await _artigoRepository.ObterPorIdAsync(id);
            if (artigo == null) return false;

            artigo.Titulo = dto.Titulo;
            artigo.Conteudo = dto.Conteudo;
            artigo.Resumo = dto.Resumo;
            artigo.ImagemUrl = dto.ImagemUrl;
            artigo.Publicado = dto.Publicado;

            await _artigoRepository.AtualizarAsync(artigo);
            return true;
        }

        public async Task<bool> DeletarAsync(int id)
        {
            var artigo = await _artigoRepository.ObterPorIdAsync(id);
            if (artigo == null) return false;

            await _artigoRepository.DeletarAsync(artigo);
            return true;
        }

        private static ArtigoResponseDto MapearParaDto(Artigo artigo)
        {
            return new ArtigoResponseDto
            {
                Id = artigo.Id,
                Titulo = artigo.Titulo,
                Conteudo = artigo.Conteudo,
                Resumo = artigo.Resumo,
                ImagemUrl = artigo.ImagemUrl,
                Publicado = artigo.Publicado,
                DataPublicacao = artigo.DataPublicacao
            };
        }

        private string GerarSlug(string titulo)
        {
            return titulo.ToLower()
                 .Replace(" ", "-")
                 .Replace("á", "a").Replace("é", "e").Replace("í", "i").Replace("ó", "o").Replace("ú", "u")
                 .Replace("ã", "a").Replace("õ", "o").Replace("ç", "c");
        }
    }
}