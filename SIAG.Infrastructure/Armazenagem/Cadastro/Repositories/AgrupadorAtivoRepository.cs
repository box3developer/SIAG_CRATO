using Microsoft.EntityFrameworkCore;
using SIAG.Application.Armazenagem.Cadastro.Services;
using SIAG.CrossCutting.DTOs;
using SIAG.CrossCutting.Interfaces;
using SIAG.CrossCutting.Utils;
using SIAG.Domain.Armazenagem.Cadastro.Interfaces;
using SIAG.Domain.Armazenagem.Core.Models;
using SIAG.Infrastructure.Configuracao;

namespace SIAG.Infrastructure.Armazenagem.Cadastro.Repositorios
{
    public class AgrupadorAtivoRepository<TEntity, TKey> : BaseRepository<TEntity, TKey>
        where TEntity : class
        where TKey : notnull
    {

        public AgrupadorAtivoRepository(SiagDbContext context) : base(context)
        {
        }

        private IQueryable<AgrupadorAtivo> FiltroPesquisa(IQueryable<AgrupadorAtivo> query, string pesquisa)
        {
            if (!string.IsNullOrWhiteSpace(pesquisa))
            {
                pesquisa = SqlUtil.GetStringTratadaWhere(pesquisa);
                query = query.Where(x => EF.Functions.Like(x.IdAgrupador.ToString(), pesquisa) ||
                                            EF.Functions.Like(x.TpAgrupamento.ToString(), pesquisa) ||
                                            EF.Functions.Like(x.CdSequencia.ToString(), pesquisa)
                                       );
            }

            return query;
        }

        public async Task<DadosPaginadosDTO<AgrupadorAtivo>> GetListAsync(FiltroPaginacaoDTO dto)
        {
            var query = _dbContext.AgrupadorAtivo.AsQueryable();
            query = FiltroPesquisa(query, dto.Pesquisa);

            var lista = await query.OrderByDescending(x => x.IdAgrupador)
                                   .GetPaged(dto.CurrentPage, dto.PageSize, dto.Impressao);

            var listaFormatada = lista.Dados.Select(x => x).ToList();

            var dadosPaginados = new DadosPaginadosDTO<AgrupadorAtivo>
            {
                Dados = listaFormatada,
                TotalPages = lista.TotalPages,
                CurrentPage = lista.CurrentPage,
                PageSize = lista.PageSize,
                TotalRegisters = lista.TotalRegisters
            };

            return dadosPaginados;
        }

        public async Task<List<SelectDTO<string>>> GetSelectAsync(FiltroPaginacaoDTO dto)
        {
            var query = _dbContext.AgrupadorAtivo.AsQueryable();

            query = FiltroPesquisa(query, dto.Pesquisa);

            query = query.OrderBy(x => x.IdAgrupador)
                         .Take(30);

            var dados = await query
                .Select(x => new SelectDTO<string>
                {
                    Id = x.IdAgrupador.ToString(),
                    Descricao = $"Agrupamento: {x.TpAgrupamento} - Sequência: {x.CdSequencia}",
                })
                .ToListAsync();

            return dados;
        }
    }
}
