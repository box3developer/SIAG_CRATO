using Microsoft.EntityFrameworkCore;
using SIAG.CrossCutting.DTOs;
using SIAG.CrossCutting.Utils;
using SIAG.Domain.Armazenagem.Cadastro.Interfaces;
using SIAG.Domain.Armazenagem.Cadastro.Models;
using SIAG.Infrastructure.Configuracao;

namespace SIAG.Infrastructure.Armazenagem.Cadastro.Repositorios
{
    public class AreaArmazenagemRepository : BaseRepository<AreaArmazenagem, long>, IAreaArmazenagemRepository
    {
        public AreaArmazenagemRepository(SiagDbContext context) : base(context)
        {
        }

        private IQueryable<AreaArmazenagem> FiltroPesquisa(IQueryable<AreaArmazenagem> query, string pesquisa)
        {
            if (!string.IsNullOrWhiteSpace(pesquisa))
            {
                pesquisa = SqlUtil.GetStringTratadaWhere(pesquisa);
                query = query.Where(x => EF.Functions.Like(x.IdAreaArmazenagem.ToString(), pesquisa) ||
                                            EF.Functions.Like(x.CdIdentificacao.ToLower(), pesquisa) ||
                                            EF.Functions.Like(x.FgStatus.ToString(), pesquisa)
                                       );
            }

            return query;
        }

        public async Task<DadosPaginadosDTO<AreaArmazenagem>> GetListAsync(FiltroPaginacaoDTO dto)
        {
            var query = _dbContext.AreaArmazenagem.AsQueryable();
            query = FiltroPesquisa(query, dto.Pesquisa);

            var lista = await query.OrderByDescending(x => x.IdAreaArmazenagem)
                                   .GetPaged(dto.CurrentPage, dto.PageSize, dto.Impressao);

            var listaFormatada = lista.Dados.Select(x => x).ToList();

            var dadosPaginados = new DadosPaginadosDTO<AreaArmazenagem>
            {
                Dados = listaFormatada,
                TotalPages = lista.TotalPages,
                CurrentPage = lista.CurrentPage,
                PageSize = lista.PageSize,
                TotalRegisters = lista.TotalRegisters
            };

            return dadosPaginados;
        }

        public async Task<List<SelectDTO<long>>> GetSelectAsync(FiltroPaginacaoDTO dto)
        {
            var query = _dbContext.AreaArmazenagem.AsQueryable();

            query = FiltroPesquisa(query, dto.Pesquisa);

            query = query.OrderBy(x => x.IdAreaArmazenagem)
                         .Take(30);

            var dados = await query
                .Select(x => new SelectDTO<long>
                {
                    Id = x.IdAreaArmazenagem,
                    Descricao = $"Identificação: {x.CdIdentificacao} - Status: {x.FgStatus} - X: {x.NrPosicaoX} - Y: {x.NrPosicaoY}",
                })
                .ToListAsync();

            return dados;
        }
    }
}
