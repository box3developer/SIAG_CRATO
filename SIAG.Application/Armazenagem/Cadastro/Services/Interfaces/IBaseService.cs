using SIAG.CrossCutting.DTOs;

namespace SIAG.Application.Armazenagem.Cadastro.Services.Interfaces
{
    public interface IBaseService<TRepository, TEntity, TKey, TDto>
    {
        Task<bool> CreateAsync(TDto dto);
        Task<bool> UpdateAsync(TDto dto);
        Task<bool> DeleteAsync(TKey id);
        Task<TDto> GetItemAsync(TKey id);
        Task<DadosPaginadosDTO<TDto>> GetListAsync(FiltroPaginacaoDTO dto);
        Task<List<SelectDTO<TKey>>> GetSelectAsync(FiltroPaginacaoDTO filtro);
    }
}
