using Microsoft.EntityFrameworkCore;
using SIAG.CrossCutting.DTOs;
using SIAG.CrossCutting.Utils;
using SIAG.Domain.Armazenagem.Cadastro.Interfaces;
using SIAG.Domain.Armazenagem.Cadastro.Models;
using SIAG.Infrastructure.Configuracao;

namespace SIAG.Infrastructure.Armazenagem.Cadastro.Repositorios
{
    public class RegiaoTrabalhoRepository : BaseRepository<RegiaoTrabalho, int>, IRegiaoTrabalhoRepository
    {
        public RegiaoTrabalhoRepository(SiagDbContext context) : base(context)
        {
        }

        private IQueryable<RegiaoTrabalho> FiltroPesquisa(IQueryable<RegiaoTrabalho> query, string pesquisa)
        {
            if (!string.IsNullOrWhiteSpace(pesquisa))
            {
                pesquisa = SqlUtil.GetStringTratadaWhere(pesquisa);
                query = query.Where(x => EF.Functions.Like(x.RegiaoTrabalhoId.ToString(), pesquisa) ||
                                            EF.Functions.Like(x.NmRegiaoTrabalho.ToLower(), pesquisa)
                                       );
            }

            return query;
        }

        public async Task<DadosPaginadosDTO<RegiaoTrabalho>> GetListAsync(FiltroPaginacaoDTO dto)
        {
            var query = _dbContext.RegiaoTrabalho.AsQueryable();
            query = FiltroPesquisa(query, dto.Pesquisa);

            var lista = await query.OrderByDescending(x => x.RegiaoTrabalhoId)
                                   .GetPaged(dto.CurrentPage, dto.PageSize, dto.Impressao);

            var listaFormatada = lista.Dados.Select(x => x).ToList();

            var dadosPaginados = new DadosPaginadosDTO<RegiaoTrabalho>
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
            var query = _dbContext.RegiaoTrabalho.AsQueryable();

            query = FiltroPesquisa(query, dto.Pesquisa);

            query = query.OrderBy(x => x.RegiaoTrabalhoId)
                         .Take(30);

            var dados = await query
                .Select(x => new SelectDTO<int>
                {
                    Id = x.RegiaoTrabalhoId,
                    Descricao = $"{x.NmRegiaoTrabalho} - Depósito: {x.DepositoId}",
                })
                .ToListAsync();

            return dados;
        }
    }
}
