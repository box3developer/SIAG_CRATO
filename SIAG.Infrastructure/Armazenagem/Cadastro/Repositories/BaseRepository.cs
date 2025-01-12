using Microsoft.EntityFrameworkCore;
using SIAG.CrossCutting.DTOs;
using SIAG.CrossCutting.Utils;
using SIAG.Domain.Armazenagem.Cadastro.Interfaces;
using SIAG.Infrastructure.Armazenagem.Cadastro.Extensions;
using SIAG.Infrastructure.Configuracao;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace SIAG.Infrastructure.Armazenagem.Cadastro.Repositorios
{
    public class BaseRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey>
        where TEntity : class
        where TKey : notnull
    {
        protected readonly SiagDbContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;
        private readonly string _keyColumn;

        public BaseRepository(SiagDbContext context)
        {
            _dbContext = context;
            _dbSet = _dbContext.Set<TEntity>();
            _keyColumn = GetKeyColumn();
        }

        private string GetKeyColumn()
        {
            var keyProperty = typeof(TEntity).GetProperties()
                                             .FirstOrDefault(p => p.GetCustomAttribute<KeyAttribute>() != null);

            if (keyProperty == null)
                throw new InvalidOperationException($"No [Key] attribute found in {typeof(TEntity).Name}");

            return keyProperty.Name;
        }

        public virtual async Task<bool> CreateAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public virtual async Task<TEntity> GetByIdAsync(TKey id)
        {
            var item = await _dbSet.FindAsync(id);

            if (item == null)
                throw new ValidacaoException("Item não encontrado");

            return item;
        }

        public virtual async Task<bool> UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public virtual async Task<bool> DeleteAsync(TKey id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null)
                throw new ValidacaoException("Item não encontrado");

            _dbSet.Remove(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<DadosPaginadosDTO<TEntity>> GetPaginatedAsync(
            Func<IQueryable<TEntity>, IQueryable<TEntity>> includeFunc,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> filterFunc,
            int currentPage,
            int pageSize,
            bool impressao)
        {
            var query = _dbContext.Set<TEntity>().AsQueryable();

            query = includeFunc?.Invoke(query) ?? query;
            query = filterFunc?.Invoke(query) ?? query;

            var lista = await query
                .OrderByDescending(x => EF.Property<object>(x, _keyColumn))
                .GetPaged(currentPage, pageSize, impressao);

            return new DadosPaginadosDTO<TEntity>
            {
                Dados = lista.Dados,
                TotalPages = lista.TotalPages,
                CurrentPage = lista.CurrentPage,
                PageSize = lista.PageSize,
                TotalRegisters = lista.TotalRegisters
            };
        }

        public virtual async Task<List<TEntity>> GetSelectAsync(
            string pesquisa,
            int quantidade,
            Dictionary<string, Func<TEntity, string>> filtros)
        {
            var query = _dbContext.Set<TEntity>().AsQueryable();

            if (!string.IsNullOrWhiteSpace(pesquisa) && filtros != null && filtros.Any())
            {
                query = query.Where(item =>
                    filtros.Any(kvp => EF.Functions.Like(kvp.Value(item).ToLower(), pesquisa)));
            }

            var dados = await query.Take(quantidade).ToListAsync();
            return dados;
        }
    }
}
