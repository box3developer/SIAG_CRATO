using SIAG.CrossCutting.DTOs;
using SIAG.Domain.Armazenagem.Cadastro.Models;

namespace SIAG.Domain.Armazenagem.Cadastro.Interfaces
{
    public interface IAreaArmazenagemRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey>
    {
        public Task<DadosPaginadosDTO<AreaArmazenagem>> GetListAsync(FiltroPaginacaoDTO dto);
        public Task<List<SelectDTO<long>>> GetSelectAsync(FiltroPaginacaoDTO dto);
    }
}
