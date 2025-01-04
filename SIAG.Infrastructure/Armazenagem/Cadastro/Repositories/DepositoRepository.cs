using Microsoft.EntityFrameworkCore;
using SIAG.CrossCutting.DTOs;
using SIAG.CrossCutting.Utils;
using SIAG.Domain.Armazenagem.Cadastro.Interfaces;
using SIAG.Domain.Armazenagem.Cadastro.Models;
using SIAG.Infrastructure.Configuracao;

namespace SIAG.Infrastructure.Armazenagem.Cadastro.Repositorios
{
    public class DepositoRepository : BaseRepository<Deposito, int>, IDepositoRepository
    {
        public DepositoRepository(SiagDbContext context) : base(context)
        {
        }

        private IQueryable<Deposito> FiltroPesquisa(IQueryable<Deposito> query, string pesquisa)
        {
            if (!string.IsNullOrWhiteSpace(pesquisa))
            {
                pesquisa = SqlUtil.GetStringTratadaWhere(pesquisa);
                query = query.Where(x => EF.Functions.Like(x.DepositoId.ToString(), pesquisa) ||
                                            EF.Functions.Like(x.NmDeposito.ToLower(), pesquisa)
                                       );
            }

            return query;
        }

        public async Task<DadosPaginadosDTO<Deposito>> GetListAsync(FiltroPaginacaoDTO dto)
        {
            var query = _dbContext.Deposito.AsQueryable();
            query = FiltroPesquisa(query, dto.Pesquisa);

            var lista = await query.OrderByDescending(x => x.DepositoId)
                                   .GetPaged(dto.CurrentPage, dto.PageSize, dto.Impressao);

            var listaFormatada = lista.Dados.Select(x => x).ToList();

            var dadosPaginados = new DadosPaginadosDTO<Deposito>
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
            var query = _dbContext.Deposito.AsQueryable();

            query = FiltroPesquisa(query, dto.Pesquisa);

            query = query.OrderBy(x => x.DepositoId)
                         .Take(30);

            var dados = await query
                .Select(x => new SelectDTO<int>
                {
                    Id = x.DepositoId,
                    Descricao = $"{x.NmDeposito}",
                })
                .ToListAsync();

            return dados;
        }
    }
}
