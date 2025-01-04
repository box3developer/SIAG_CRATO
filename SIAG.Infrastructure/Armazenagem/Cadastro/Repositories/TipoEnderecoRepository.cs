using Microsoft.EntityFrameworkCore;
using SIAG.CrossCutting.DTOs;
using SIAG.CrossCutting.Utils;
using SIAG.Domain.Armazenagem.Cadastro.Interfaces;
using SIAG.Domain.Armazenagem.Cadastro.Models;
using SIAG.Infrastructure.Configuracao;

namespace SIAG.Infrastructure.Armazenagem.Cadastro.Repositorios
{
    public class TipoEnderecoRepository : BaseRepository<TipoEndereco, int>, ITipoEnderecoRepository
    {
        public TipoEnderecoRepository(SiagDbContext context) : base(context)
        {
        }

        private IQueryable<TipoEndereco> FiltroPesquisa(IQueryable<TipoEndereco> query, string pesquisa)
        {
            if (!string.IsNullOrWhiteSpace(pesquisa))
            {
                pesquisa = SqlUtil.GetStringTratadaWhere(pesquisa);
                query = query.Where(x => EF.Functions.Like(x.TipoEnderecoId.ToString(), pesquisa) ||
                                            EF.Functions.Like(x.NmTipoEndereco.ToLower(), pesquisa)
                                       );
            }

            return query;
        }

        public async Task<DadosPaginadosDTO<TipoEndereco>> GetListAsync(FiltroPaginacaoDTO dto)
        {
            var query = _dbContext.TipoEndereco.AsQueryable();
            query = FiltroPesquisa(query, dto.Pesquisa);

            var lista = await query.OrderByDescending(x => x.TipoEnderecoId)
                                   .GetPaged(dto.CurrentPage, dto.PageSize, dto.Impressao);

            var listaFormatada = lista.Dados.Select(x => x).ToList();

            var dadosPaginados = new DadosPaginadosDTO<TipoEndereco>
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
            var query = _dbContext.TipoEndereco.AsQueryable();

            query = FiltroPesquisa(query, dto.Pesquisa);

            query = query.OrderBy(x => x.TipoEnderecoId)
                         .Take(30);

            var dados = await query
                .Select(x => new SelectDTO<int>
                {
                    Id = x.TipoEnderecoId,
                    Descricao = $"{x.NmTipoEndereco}",
                })
                .ToListAsync();

            return dados;
        }
    }
}
