using Microsoft.EntityFrameworkCore;
using SIAG.Application.Armazenagem.Cadastro.DTOs;
using SIAG.CrossCutting.DTOs;
using SIAG.CrossCutting.Interfaces;
using SIAG.CrossCutting.Logging;
using SIAG.CrossCutting.Services;
using SIAG.CrossCutting.Utils;
using SIAG.Domain.Armazenagem.Cadastro.Interfaces;
using SIAG.Domain.Armazenagem.Cadastro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIAG.Application.Armazenagem.Cadastro.Services
{
    public class AgrupadorAtivoService : BaseService<IAgrupadorAtivoRepository, AgrupadorAtivo, AgrupadorAtivoDTO, string>
    {
        private readonly IAgrupadorAtivoRepository _repository;
        private readonly IMappingService _mappingService;

        public AgrupadorAtivoService(IAgrupadorAtivoRepository repository, IMappingService mappingService) : base(repository, mappingService)
        {
            _repository = repository;
            _mappingService = mappingService;
        }

        public async Task<DadosPaginadosDTO<AgrupadorAtivoDTO>> GetListAsync(FiltroPaginacaoDTO dto)
        {
            var lista = await _repository.GetListAsync(dto);

            var listaFormatada = lista.Dados.Select(x => _mappingService.Map<AgrupadorAtivo, AgrupadorAtivoDTO>(x)).ToList();

            return new DadosPaginadosDTO<AgrupadorAtivoDTO>
            {
                Dados = listaFormatada,
                TotalPages = lista.TotalPages,
                CurrentPage = lista.CurrentPage,
                PageSize = lista.PageSize,
                TotalRegisters = lista.TotalRegisters
            };
        }

        public async Task<List<SelectDTO<string>>> GetSelectAsync(FiltroPaginacaoDTO filtro)
        {
            var lista = await _repository.GetSelectAsync(filtro);

            return lista;
        }
    }
}