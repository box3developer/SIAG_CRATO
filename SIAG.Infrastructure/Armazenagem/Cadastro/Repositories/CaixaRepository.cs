using Microsoft.EntityFrameworkCore;
using SIAG.CrossCutting.DTOs;
using SIAG.CrossCutting.Utils;
using SIAG.Domain.Armazenagem.Cadastro.Interfaces;
using SIAG.Domain.Armazenagem.Cadastro.Models;
using SIAG.Infrastructure.Configuracao;

namespace SIAG.Infrastructure.Armazenagem.Cadastro.Repositorios
{
    public class CaixaRepository : BaseRepository<Caixa, string>, ICaixaRepository
    {
        public CaixaRepository(SiagDbContext context) : base(context)
        {
        }

        private IQueryable<Caixa> FiltroPesquisa(IQueryable<Caixa> query, string pesquisa)
        {
            if (!string.IsNullOrWhiteSpace(pesquisa))
            {
                pesquisa = SqlUtil.GetStringTratadaWhere(pesquisa);
                query = query.Where(x => EF.Functions.Like(x.IdCaixa.ToString(), pesquisa) ||
                                            EF.Functions.Like(x.CdProduto.ToLower(), pesquisa)
                                       );
            }

            return query;
        }

        public async Task<DadosPaginadosDTO<Caixa>> GetListAsync(FiltroPaginacaoDTO dto)
        {
            var query = _dbContext.Caixa.AsQueryable();
            query = FiltroPesquisa(query, dto.Pesquisa);

            var lista = await query.OrderByDescending(x => x.IdCaixa)
                                   .GetPaged(dto.CurrentPage, dto.PageSize, dto.Impressao);

            var listaFormatada = lista.Dados.Select(x => x).ToList();

            var dadosPaginados = new DadosPaginadosDTO<Caixa>
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
            var query = _dbContext.Caixa.AsQueryable();

            query = FiltroPesquisa(query, dto.Pesquisa);

            query = query.OrderBy(x => x.IdCaixa)
                         .Take(30);

            var dados = await query
                .Select(x => new SelectDTO<string>
                {
                    Id = x.IdCaixa,
                    Descricao = $"{x.CdCor}",
                })
                .ToListAsync();

            return dados;
        }
    }
}
