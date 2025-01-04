using Microsoft.EntityFrameworkCore;
using SIAG.CrossCutting.DTOs;
using SIAG.CrossCutting.Utils;
using SIAG.Domain.Armazenagem.Cadastro.Interfaces;
using SIAG.Domain.Armazenagem.Cadastro.Models;
using SIAG.Infrastructure.Configuracao;

namespace SIAG.Infrastructure.Armazenagem.Cadastro.Repositorios
{
    public class PalletRepository : BaseRepository<Pallet, int>, IPalletRepository
    {
        public PalletRepository(SiagDbContext context) : base(context)
        {
        }

        private IQueryable<Pallet> FiltroPesquisa(IQueryable<Pallet> query, string pesquisa)
        {
            if (!string.IsNullOrWhiteSpace(pesquisa))
            {
                pesquisa = SqlUtil.GetStringTratadaWhere(pesquisa);
                query = query.Where(x => EF.Functions.Like(x.PalletId.ToString(), pesquisa) ||
                                            EF.Functions.Like(x.CdIdentificacao.ToString(), pesquisa)
                                       );
            }

            return query;
        }

        public async Task<DadosPaginadosDTO<Pallet>> GetListAsync(FiltroPaginacaoDTO dto)
        {
            var query = _dbContext.Pallet.AsQueryable();
            query = FiltroPesquisa(query, dto.Pesquisa);

            var lista = await query.OrderByDescending(x => x.PalletId)
                                   .GetPaged(dto.CurrentPage, dto.PageSize, dto.Impressao);

            var listaFormatada = lista.Dados.Select(x => x).ToList();

            var dadosPaginados = new DadosPaginadosDTO<Pallet>
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
            var query = _dbContext.Pallet.AsQueryable();

            query = FiltroPesquisa(query, dto.Pesquisa);

            query = query.OrderBy(x => x.PalletId)
                         .Take(30);

            var dados = await query
                .Select(x => new SelectDTO<int>
                {
                    Id = x.PalletId,
                    Descricao = $"Área Armaz.: {x.AreaArmazenagemId} - Identificação: {x.CdIdentificacao}",
                })
                .ToListAsync();

            return dados;
        }
    }
}
