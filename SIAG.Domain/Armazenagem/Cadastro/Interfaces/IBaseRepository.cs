using SIAG.CrossCutting.DTOs;

namespace SIAG.Domain.Armazenagem.Cadastro.Interfaces
{
    public interface IBaseRepository<TEntity, TKey>
    {
        Task<bool> CreateAsync(TEntity dto);
        Task<TEntity> GetByIdAsync(TKey id);
        Task<bool> UpdateAsync(TEntity dto);
        Task<bool> DeleteAsync(TKey id);
    }
}
