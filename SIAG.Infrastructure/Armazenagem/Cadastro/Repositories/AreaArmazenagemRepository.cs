using Microsoft.EntityFrameworkCore;
using SIAG.Application.Armazenagem.Cadastro.DTOs;
using SIAG.CrossCutting.DTOs;
using SIAG.CrossCutting.Utils;
using SIAG.Domain.Armazenagem.Cadastro.Interfaces;
using SIAG.Domain.Armazenagem.Cadastro.Models;
using SIAG.Infrastructure.Configuracao;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SIAG.Infrastructure.Armazenagem.Cadastro.Repositorios
{
    public class AreaArmazenagemRepository : BaseRepository<AreaArmazenagem, int>, IAreaArmazenagemRepository
    {
        public AreaArmazenagemRepository(SiagDbContext context) : base(context)
        {
        }

        private IQueryable<AreaArmazenagem> FiltroPesquisa(IQueryable<AreaArmazenagem> query, string pesquisa)
        {
            if (!string.IsNullOrWhiteSpace(pesquisa))
            {
                pesquisa = SqlUtil.GetStringTratadaWhere(pesquisa);
                query = query.Where(x => EF.Functions.Like(x.AreaArmazenagemId.ToString(), pesquisa) ||
                                            EF.Functions.Like(x.CdIdentificacao.ToLower(), pesquisa) ||
                                            EF.Functions.Like(x.FgStatus.ToString(), pesquisa)
                                       );
            }

            return query;
        }

        public async Task<List<AreaArmazenagem>> GetListAsync(FiltroPaginacaoDTO dto)
        {
            var query = _dbContext.AreaArmazenagem.AsQueryable();
            query = FiltroPesquisa(query, dto.Pesquisa);

            return await query.ToListAsync();
        }
    }
}
