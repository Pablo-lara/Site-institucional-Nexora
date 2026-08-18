using System;
using System.Collections.Generic;
using System.Text;
using Nexora.Application.DTOs.Servicos;
using Nexora.Application.Interfaces.Repositories;
using Nexora.Application.Interfaces.Services;
using Nexora.Domain.Entities;
using Nexora.Application.Exceptions;

namespace Nexora.Application.Services
{
    public class ServicoService : IServicoService
    {
        private readonly IServicoRepository _repository;

        public ServicoService(IServicoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ServicoResponseDto>> ObterTodosAsync()
        {
            var servicos = await _repository.ObterTodosAsync();

            return servicos.Select(MapearParaResponse);
        }

        public async Task<ServicoResponseDto?> ObterPorIdAsync(int id)
        {
            var servico = await _repository.ObterPorIdAsync(id);

            if (servico is null)
                return null;

            return MapearParaResponse(servico);
        }

        public async Task<ServicoResponseDto> CriarAsync(
            CriarServicoDto dto)
        {

            var nomeJaExiste = await _repository.ExistePorNomeAsync(dto.Nome);

            if (nomeJaExiste)
            {
                throw new RegraNegocioException(
                    "Já existe um serviço cadastrado com esse nome.");
            }

            var servico = new Servico
            {
                Nome = dto.Nome,
                Descricao = dto.Descricao,
                Preco = dto.Preco,
                ImagemUrl = dto.ImagemUrl,
                Ativo = dto.Ativo,
                DataCriacao = DateTime.UtcNow
            };

            var servicoCriado = await _repository.CriarAsync(servico);

            return MapearParaResponse(servicoCriado);
        }

        public async Task<bool> AtualizarAsync(
            int id,
            AtualizarServicoDto dto)
        {
            var servico = await _repository.ObterPorIdAsync(id);

            if (servico is null)
                return false;

            servico.Nome = dto.Nome;
            servico.Descricao = dto.Descricao;
            servico.Preco = dto.Preco;
            servico.ImagemUrl = dto.ImagemUrl;
            servico.Ativo = dto.Ativo;
            servico.DataAtualizacao = DateTime.UtcNow;

            await _repository.AtualizarAsync(servico);

            return true;
        }

        public async Task<bool> RemoverAsync(int id)
        {
            var servico = await _repository.ObterPorIdAsync(id);

            if (servico is null)
                return false;

            await _repository.RemoverAsync(servico);

            return true;
        }

        private static ServicoResponseDto MapearParaResponse(
            Servico servico)
        {
            return new ServicoResponseDto
            {
                Id = servico.Id,
                Nome = servico.Nome,
                Descricao = servico.Descricao,
                Preco = servico.Preco,
                ImagemUrl = servico.ImagemUrl,
                Ativo = servico.Ativo,
                DataCriacao = servico.DataCriacao,
                DataAtualizacao = servico.DataAtualizacao
            };
        }
    }
}
