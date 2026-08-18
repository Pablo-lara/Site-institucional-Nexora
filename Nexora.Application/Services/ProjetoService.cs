using Nexora.Application.DTOs.Projetos;
using Nexora.Application.Exceptions;
using Nexora.Application.Interfaces.Repositories;
using Nexora.Application.Interfaces.Services;
using Nexora.Domain.Entities;

namespace Nexora.Application.Services;

public class ProjetoService : IProjetoService
{
    private readonly IProjetoRepository _repository;

    public ProjetoService(IProjetoRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ProjetoResponseDto>> ObterTodosAsync()
    {
        var projetos = await _repository.ObterTodosAsync();

        return projetos.Select(MapearParaResponse);
    }

    public async Task<IEnumerable<ProjetoResponseDto>> ObterDestaquesAsync()
    {
        var projetos = await _repository.ObterDestaquesAsync();

        return projetos.Select(MapearParaResponse);
    }

    public async Task<ProjetoResponseDto?> ObterPorIdAsync(int id)
    {
        var projeto = await _repository.ObterPorIdAsync(id);

        if (projeto is null)
            return null;

        return MapearParaResponse(projeto);
    }

    public async Task<ProjetoResponseDto> CriarAsync(
        CriarProjetoDto dto)
    {
        var nomeJaExiste =
            await _repository.ExistePorNomeAsync(dto.Nome);

        if (nomeJaExiste)
        {
            throw new RegraNegocioException(
                "Já existe um projeto cadastrado com esse nome.");
        }

        var projeto = new Projeto
        {
            Nome = dto.Nome,
            Descricao = dto.Descricao,
            ImagemUrl = dto.ImagemUrl,
            UrlProjeto = dto.UrlProjeto,
            Cliente = dto.Cliente ?? string.Empty,
            Tecnologias = dto.Tecnologias,
            Destaque = dto.Destaque,
            Ativo = dto.Ativo,
            DataCriacao = DateTime.UtcNow
        };

        var projetoCriado =
            await _repository.CriarAsync(projeto);

        return MapearParaResponse(projetoCriado);
    }

    public async Task<bool> AtualizarAsync(
        int id,
        AtualizarProjetoDto dto)
    {
        var projeto = await _repository.ObterPorIdAsync(id);

        if (projeto is null)
            return false;

        var nomeJaExiste =
            await _repository.ExistePorNomeAsync(dto.Nome);

        if (nomeJaExiste && projeto.Nome != dto.Nome)
        {
            throw new RegraNegocioException(
                "Já existe outro projeto cadastrado com esse nome.");
        }

        projeto.Nome = dto.Nome;
        projeto.Cliente = dto.Cliente;
        projeto.Descricao = dto.Descricao;
        projeto.ImagemUrl = dto.ImagemUrl;
        projeto.UrlProjeto = dto.UrlProjeto;
        projeto.Tecnologias = dto.Tecnologias;
        projeto.Destaque = dto.Destaque;
        projeto.Ativo = dto.Ativo;
        projeto.DataAtualizacao = DateTime.UtcNow;

        await _repository.AtualizarAsync(projeto);

        return true;
    }

    public async Task<bool> RemoverAsync(int id)
    {
        var projeto = await _repository.ObterPorIdAsync(id);

        if (projeto is null)
            return false;

        await _repository.RemoverAsync(projeto);

        return true;
    }

    private static ProjetoResponseDto MapearParaResponse(
        Projeto projeto)
    {
        return new ProjetoResponseDto
        {
            Id = projeto.Id,
            Nome = projeto.Nome,
            Descricao = projeto.Descricao,
            ImagemUrl = projeto.ImagemUrl,
            UrlProjeto = projeto.UrlProjeto,
            Cliente = projeto.Cliente,
            Tecnologias = projeto.Tecnologias,
            Destaque = projeto.Destaque,
            Ativo = projeto.Ativo,
            DataCriacao = projeto.DataCriacao,
            DataAtualizacao = projeto.DataAtualizacao
        };
    }
}