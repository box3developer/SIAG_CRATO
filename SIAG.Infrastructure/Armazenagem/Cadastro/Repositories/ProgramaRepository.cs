using Microsoft.EntityFrameworkCore;
using SIAG.CrossCutting.DTOs;
using SIAG.CrossCutting.Utils;
using SIAG.Domain.Armazenagem.Cadastro.Interfaces;
using SIAG.Domain.Armazenagem.Cadastro.Models;
using SIAG.Infrastructure.Configuracao;

namespace SIAG.Infrastructure.Armazenagem.Cadastro.Repositorios
{
    public class ProgramaRepository : BaseRepository<Programa, int>, IProgramaRepository
    {
        public ProgramaRepository(SiagDbContext context) : base(context)
        {
        }

        private IQueryable<Programa> FiltroPesquisa(IQueryable<Programa> query, string pesquisa)
        {
            if (!string.IsNullOrWhiteSpace(pesquisa))
            {
                pesquisa = SqlUtil.GetStringTratadaWhere(pesquisa);
                query = query.Where(x => EF.Functions.Like(x.ProgramaId.ToString(), pesquisa) ||
                                            EF.Functions.Like(x.CdPrograma.ToString(), pesquisa)
                                       );
            }

            return query;
        }

        public async Task<DadosPaginadosDTO<Programa>> GetListAsync(FiltroPaginacaoDTO dto)
        {
            var query = _dbContext.Programa.AsQueryable();
            query = FiltroPesquisa(query, dto.Pesquisa);

            var lista = await query.OrderByDescending(x => x.ProgramaId)
                                   .GetPaged(dto.CurrentPage, dto.PageSize, dto.Impressao);

            var listaFormatada = lista.Dados.Select(x => x).ToList();

            var dadosPaginados = new DadosPaginadosDTO<Programa>
            {
                Dados = listaFormatada,
                TotalPages = lista.TotalPages,
                CurrentPage = lista.CurrentPage,
                PageSize = lista.PageSize,
                TotalRegisters = lista.TotalRegisters
            };

            return dadosPaginados;
        }

        public async Task<List<SelectDTO<int>>> GetSelectAsync(FiltroPaginacaoDTO dto)
        {
            var query = _dbContext.Programa.AsQueryable();

            query = FiltroPesquisa(query, dto.Pesquisa);

            query = query.OrderBy(x => x.ProgramaId)
                         .Take(30);

            var dados = await query
                .Select(x => new SelectDTO<int>
                {
                    Id = x.ProgramaId,
                    Descricao = $"{x.CdPrograma}",
                })
                .ToListAsync();

            return dados;
        }
    }
}
