using Microsoft.EntityFrameworkCore;
using SIAG.Application.Armazenagem.Cadastro.DTOs;
using SIAG.CrossCutting.DTOs;
using SIAG.CrossCutting.Interfaces;
using SIAG.CrossCutting.Logging;
using SIAG.CrossCutting.Utils;
using SIAG.Domain.Armazenagem.Cadastro.Interfaces;
using SIAG.Domain.Armazenagem.Cadastro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIAG.Application.Armazenagem.Cadastro.Services
{
    public class AreaArmazenagemService : BaseService<IAreaArmazenagemRepository, AreaArmazenagem, AreaArmazenagemDTO, int>
    {
        public AreaArmazenagemService(
            IAreaArmazenagemRepository repository,
            IMappingService mappingService)
            : base(repository, mappingService)
        {
        }

        public async Task<DadosPaginadosDTO<AreaArmazenagemDTO>> GetListAsync(FiltroPaginacaoDTO dto)
        {
            var paginatedData = await _areaArmazenagemRepository.GetListAsync(dto);

            return new DadosPaginadosDTO<AreaArmazenagemDTO>
            {
                Dados = paginatedData.Select(ConvertToDTO).ToList(),
                TotalPages = paginatedData.TotalPages,
                CurrentPage = paginatedData.CurrentPage,
                PageSize = paginatedData.PageSize,
                TotalRegisters = paginatedData.TotalRegisters
            };
        }

        public async Task<List<SelectDTO<int>>> GetSelectAsync(FiltroPaginacaoDTO filtro)
        {
            var lista = await _areaArmazenagemRepository.GetListAsync(filtro);

            return lista.Select(x => new SelectDTO<int>
            {
                Id = x.AreaArmazenagemId,
                Descricao = $"Identificação: {x.CdIdentificacao} - Status: {x.FgStatus} - X: {x.NrPosicaoX} - Y: {x.NrPosicaoY}"
            }).ToList();
        }
    }
}
