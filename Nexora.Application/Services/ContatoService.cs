using Nexora.Application.DTOs.Contatos;
using Nexora.Application.Interfaces.Repositories;
using Nexora.Application.Interfaces.Services;
using Nexora.Domain.Entities;

namespace Nexora.Application.Services
{
    public class ContatoService : IContatoService
    {
        private readonly IContatoRepository _contatoRepository;

        public ContatoService(IContatoRepository contatoRepository)
        {
            _contatoRepository = contatoRepository;
        }

        public async Task<IEnumerable<ContatoResponseDto>> ObterTodosAsync()
        {
            var contatos = await _contatoRepository.ObterTodosAsync();
            return contatos.Select(MapearParaDto);
        }

        public async Task<ContatoResponseDto?> ObterPorIdAsync(int id)
        {
            var contato = await _contatoRepository.ObterPorIdAsync(id);
            return contato == null ? null : MapearParaDto(contato);
        }

        public async Task<ContatoResponseDto> EnviarAsync(EnviarContatoDto dto)
        {
            var contato = new Contato
            {
                Nome = dto.Nome,
                Email = dto.Email,
                Telefone = dto.Telefone,
                Assunto = dto.Assunto,
                Mensagem = dto.Mensagem,
                Lido = false,
                DataEnvio = DateTime.UtcNow
            };

            var criado = await _contatoRepository.CriarAsync(contato);
            return MapearParaDto(criado);
        }

        public async Task<bool> MarcarComoLidaAsync(int id)
        {
            var contato = await _contatoRepository.ObterPorIdAsync(id);
            if (contato == null) return false;

            contato.Lido = true;
            await _contatoRepository.AtualizarAsync(contato);
            return true;
        }

        public async Task<bool> DeletarAsync(int id)
        {
            var contato = await _contatoRepository.ObterPorIdAsync(id);
            if (contato == null) return false;

            await _contatoRepository.DeletarAsync(contato);
            return true;
        }

        private static ContatoResponseDto MapearParaDto(Contato contato)
        {
            return new ContatoResponseDto
            {
                Id = contato.Id,
                Nome = contato.Nome,
                Email = contato.Email,
                Telefone = contato.Telefone,
                Assunto = contato.Assunto,
                Mensagem = contato.Mensagem,
                Lida = contato.Lido,
                DataEnvio = contato.DataEnvio
            };
        }
    }
}