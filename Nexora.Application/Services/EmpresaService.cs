using Nexora.Application.DTOs.Empresa;
using Nexora.Application.Interfaces.Repositories;
using Nexora.Application.Interfaces.Services;
using Nexora.Domain.Entities;

namespace Nexora.Application.Services
{
    public class EmpresaService : IEmpresaService
    {
        private readonly IEmpresaRepository _empresaRepository;

        public EmpresaService(IEmpresaRepository empresaRepository)
        {
            _empresaRepository = empresaRepository;
        }

        public async Task<EmpresaResponseDto?> ObterInformacoesAsync()
        {
            var empresa = await _empresaRepository.ObterDadosEmpresaAsync();
            return empresa == null ? null : MapearParaDto(empresa);
        }

        public async Task<EmpresaResponseDto> SalvarInformacoesAsync(AtualizarEmpresaDto dto)
        {
            var empresa = new Empresa
            {
                Nome = dto.Nome,
                Descricao = dto.SobreNos,
                Email = dto.EmailContato,
                Telefone = dto.Telefone,
                Endereco = dto.Endereco,
                Linkedin = dto.LinkedinUrl,
                Instagram = dto.InstagramUrl,
                Github = dto.GithubUrl
            };

            var salva = await _empresaRepository.CriarOuAtualizarAsync(empresa);
            return MapearParaDto(salva);
        }

        private static EmpresaResponseDto MapearParaDto(Empresa empresa)
        {
            return new EmpresaResponseDto
            {
                Id = empresa.Id,
                Nome = empresa.Nome,
                SobreNos = empresa.Descricao,
                EmailContato = empresa.Email,
                Telefone = empresa.Telefone,
                Endereco = empresa.Endereco,
                LinkedinUrl = empresa.Linkedin,
                InstagramUrl = empresa.Instagram,
                GithubUrl = empresa.Github
            };
        }
    }
}