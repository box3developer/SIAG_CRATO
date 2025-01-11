using Microsoft.EntityFrameworkCore;
using SIAG.CrossCutting.DTOs;
using SIAG.CrossCutting.Utils;
using SIAG.Domain.Armazenagem.Cadastro.Interfaces;
using SIAG.Infrastructure.Configuracao;

namespace SIAG.Infrastructure.Armazenagem.Cadastro.Repositorios
{
    public class BaseRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey>
        where TEntity : class
        where TKey : class
    {
        protected readonly SiagDbContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;

        public BaseRepository(SiagDbContext context)
        {
            _dbContext = context;
            _dbSet = _dbContext.Set<TEntity>();
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
        
        public virtual async Task<DadosPaginadosDTO<TEntity>> GetListAsync(FiltroPaginacaoDTO dto)
        {
            return new DadosPaginadosDTO<TEntity> { };
        }

        public virtual async Task<List<SelectDTO<TKey>>> GetSelectAsync(FiltroPaginacaoDTO filtro)
        {
            return new List<SelectDTO<TKey>> { };
        }
    }
}
