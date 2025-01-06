using Microsoft.EntityFrameworkCore;
using SIAG.CrossCutting.DTOs;
using SIAG.CrossCutting.Utils;
using SIAG.Domain.Armazenagem.Cadastro.Interfaces;
using SIAG.Domain.Armazenagem.Cadastro.Models;
using SIAG.Infrastructure.Configuracao;

namespace SIAG.Infrastructure.Armazenagem.Cadastro.Repositorios
{
    public class PedidoRepository : BaseRepository<Pedido, int>, IPedidoRepository
    {
        public PedidoRepository(SiagDbContext context) : base(context)
        {
        }

        private IQueryable<Pedido> FiltroPesquisa(IQueryable<Pedido> query, string pesquisa)
        {
            if (!string.IsNullOrWhiteSpace(pesquisa))
            {
                pesquisa = SqlUtil.GetStringTratadaWhere(pesquisa);
                query = query.Where(x => EF.Functions.Like(x.IdPedido.ToString(), pesquisa) ||
                                            EF.Functions.Like(x.CdPedido.ToString(), pesquisa)
                                       );
            }

            return query;
        }

        public async Task<DadosPaginadosDTO<Pedido>> GetListAsync(FiltroPaginacaoDTO dto)
        {
            var query = _dbContext.Pedido.AsQueryable();
            query = FiltroPesquisa(query, dto.Pesquisa);

            var lista = await query.OrderByDescending(x => x.IdPedido)
                                   .GetPaged(dto.CurrentPage, dto.PageSize, dto.Impressao);

            var listaFormatada = lista.Dados.Select(x => x).ToList();

            var dadosPaginados = new DadosPaginadosDTO<Pedido>
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
            var query = _dbContext.Pedido.AsQueryable();

            query = FiltroPesquisa(query, dto.Pesquisa);

            query = query.OrderBy(x => x.IdPedido)
                         .Take(30);

            var dados = await query
                .Select(x => new SelectDTO<int>
                {
                    Id = x.IdPedido,
                    Descricao = $"{x.CdPedido}",
                })
                .ToListAsync();

            return dados;
        }
    }
}
