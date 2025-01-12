using SIAG.CrossCutting.DTOs;

namespace SIAG.Domain.Armazenagem.Cadastro.Interfaces
{
    public interface IBaseRepository<TEntity, TKey>
    {
        Task<bool> CreateAsync(TEntity dto);
        Task<TEntity> GetByIdAsync(TKey id);
        Task<bool> UpdateAsync(TEntity dto);
        Task<bool> DeleteAsync(TKey id);
        Task<DadosPaginadosDTO<TEntity>> GetPaginatedAsync(
            Func<IQueryable<TEntity>, IQueryable<TEntity>> includeFunc,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> filterFunc,
            int currentPage,
            int pageSize,
            bool impressao);
        Task<List<TEntity>> GetSelectAsync(
            string pesquisa,
            int quantidade,
            Dictionary<string, Func<TEntity, string>> filtros);
    }
}
