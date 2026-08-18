using Nexora.Application.DTOs.Orcamentos;
using Nexora.Application.Interfaces.Repositories;
using Nexora.Application.Interfaces.Services;
using Nexora.Domain.Entities;
using Nexora.Domain.Enums;

namespace Nexora.Application.Services
{
    public class OrcamentoService : IOrcamentoService
    {
        private readonly IOrcamentoRepository _orcamentoRepository;

        public OrcamentoService(IOrcamentoRepository orcamentoRepository)
        {
            _orcamentoRepository = orcamentoRepository;
        }

        public async Task<IEnumerable<OrcamentoResponseDto>> ObterTodosAsync()
        {
            var orcamentos = await _orcamentoRepository.ObterTodosAsync();
            return orcamentos.Select(MapearParaDto);
        }

        public async Task<OrcamentoResponseDto?> ObterPorIdAsync(int id)
        {
            var orcamento = await _orcamentoRepository.ObterPorIdAsync(id);
            return orcamento == null ? null : MapearParaDto(orcamento);
        }

        public async Task<OrcamentoResponseDto> SolicitarAsync(SolicitarOrcamentoDto dto)
        {
            var orcamento = new Orcamento
            {
                NomeCliente = dto.NomeCliente,
                Email = dto.Email,
                Telefone = dto.Telefone,
                //NomeEmpresa = dto.NomeEmpresa,
                ServicoId = dto.ServicoId,
                Descricao = dto.Descricao,
                Status = StatusOrcamento.Pendente,
                DataSolicitacao = DateTime.UtcNow
            };

            var criado = await _orcamentoRepository.CriarAsync(orcamento);

            // Reobter do banco para carregar o Navigation Property do Servico (se informado)
            var orcamentoCarregado = await _orcamentoRepository.ObterPorIdAsync(criado.Id);
            return MapearParaDto(orcamentoCarregado ?? criado);
        }

        public async Task<bool> AtualizarStatusAsync(int id, AtualizarStatusOrcamentoDto dto)
        {
            var orcamento = await _orcamentoRepository.ObterPorIdAsync(id);
            if (orcamento == null) return false;

            orcamento.Status = dto.Status;
            if (dto.ObservacoesAdmin != null)
            {
                orcamento.ObservacoesAdmin = dto.ObservacoesAdmin;
            }

            await _orcamentoRepository.AtualizarAsync(orcamento);
            return true;
        }

        public async Task<bool> DeletarAsync(int id)
        {
            var orcamento = await _orcamentoRepository.ObterPorIdAsync(id);
            if (orcamento == null) return false;

            await _orcamentoRepository.DeletarAsync(orcamento);
            return true;
        }

        private static OrcamentoResponseDto MapearParaDto(Orcamento orcamento)
        {
            return new OrcamentoResponseDto
            {
                Id = orcamento.Id,
                NomeCliente = orcamento.NomeCliente,
                Email = orcamento.Email,
                Telefone = orcamento.Telefone,
                //NomeEmpresa = orcamento.NomeEmpresa,
                ServicoId = orcamento.ServicoId,
                NomeServico = orcamento.Servico?.Nome,
                Descricao = orcamento.Descricao,
                Status = orcamento.Status,
                ObservacoesAdmin = orcamento.ObservacoesAdmin,
                DataSolicitacao = orcamento.DataSolicitacao
            };
        }
    }
}