using Microsoft.EntityFrameworkCore;
using SIAG.CrossCutting.DTOs;
using SIAG.CrossCutting.Utils;
using SIAG.Domain.Armazenagem.Cadastro.Interfaces;
using SIAG.Domain.Armazenagem.Cadastro.Models;
using SIAG.Infrastructure.Configuracao;

namespace SIAG.Infrastructure.Armazenagem.Cadastro.Repositorios
{
    public class EquipamentoRepository : BaseRepository<Equipamento, int>, IEquipamentoRepository
    {
        public EquipamentoRepository(SiagDbContext context) : base(context)
        {
        }

        private IQueryable<Equipamento> FiltroPesquisa(IQueryable<Equipamento> query, string pesquisa)
        {
            if (!string.IsNullOrWhiteSpace(pesquisa))
            {
                pesquisa = SqlUtil.GetStringTratadaWhere(pesquisa);
                query = query.Where(x => EF.Functions.Like(x.IdEquipamento.ToString(), pesquisa) ||
                                            EF.Functions.Like(x.NmEquipamento.ToLower(), pesquisa)
                                       );
            }

            return query;
        }

        public async Task<DadosPaginadosDTO<Equipamento>> GetListAsync(FiltroPaginacaoDTO dto)
        {
            var query = _dbContext.Equipamento.AsQueryable();
            query = FiltroPesquisa(query, dto.Pesquisa);

            var lista = await query.OrderByDescending(x => x.IdEquipamento)
                                   .GetPaged(dto.CurrentPage, dto.PageSize, dto.Impressao);

            var listaFormatada = lista.Dados.Select(x => x).ToList();

            var dadosPaginados = new DadosPaginadosDTO<Equipamento>
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
            var query = _dbContext.Equipamento.AsQueryable();

            query = FiltroPesquisa(query, dto.Pesquisa);

            query = query.OrderBy(x => x.IdEquipamento)
                         .Take(30);

            var dados = await query
                .Select(x => new SelectDTO<int>
                {
                    Id = x.IdEquipamento,
                    Descricao = $"{x.NmEquipamento}",
                })
                .ToListAsync();

            return dados;
        }
    }
}
