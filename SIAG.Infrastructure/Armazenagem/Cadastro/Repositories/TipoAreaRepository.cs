using Microsoft.EntityFrameworkCore;
using SIAG.CrossCutting.DTOs;
using SIAG.CrossCutting.Utils;
using SIAG.Domain.Armazenagem.Cadastro.Interfaces;
using SIAG.Domain.Armazenagem.Cadastro.Models;
using SIAG.Infrastructure.Configuracao;

namespace SIAG.Infrastructure.Armazenagem.Cadastro.Repositorios
{
    public class TipoAreaRepository : BaseRepository<TipoArea, int>, ITipoAreaRepository
    {
        public TipoAreaRepository(SiagDbContext context) : base(context)
        {
        }

        private IQueryable<TipoArea> FiltroPesquisa(IQueryable<TipoArea> query, string pesquisa)
        {
            if (!string.IsNullOrWhiteSpace(pesquisa))
            {
                pesquisa = SqlUtil.GetStringTratadaWhere(pesquisa);
                query = query.Where(x => EF.Functions.Like(x.IdTipoArea.ToString(), pesquisa) ||
                                            EF.Functions.Like(x.NmTipoArea.ToLower(), pesquisa)
                                       );
            }

            return query;
        }

        public async Task<DadosPaginadosDTO<TipoArea>> GetListAsync(FiltroPaginacaoDTO dto)
        {
            var query = _dbContext.TipoArea.AsQueryable();
            query = FiltroPesquisa(query, dto.Pesquisa);

            var lista = await query.OrderByDescending(x => x.IdTipoArea)
                                   .GetPaged(dto.CurrentPage, dto.PageSize, dto.Impressao);

            var listaFormatada = lista.Dados.Select(x => x).ToList();

            var dadosPaginados = new DadosPaginadosDTO<TipoArea>
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
            var query = _dbContext.TipoArea.AsQueryable();

            query = FiltroPesquisa(query, dto.Pesquisa);

            query = query.OrderBy(x => x.IdTipoArea)
                         .Take(30);

            var dados = await query
                .Select(x => new SelectDTO<int>
                {
                    Id = x.IdTipoArea,
                    Descricao = $"{x.IdTipoArea}",
                })
                .ToListAsync();

            return dados;
        }
    }
}
