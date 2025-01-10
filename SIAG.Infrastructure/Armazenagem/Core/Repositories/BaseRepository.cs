using Microsoft.EntityFrameworkCore;
using SIAG.CrossCutting.Utils;
using SIAG.Domain.Armazenagem.Core.Interfaces;
using SIAG.Infrastructure.Configuracao;

namespace SIAG.Infrastructure.Armazenagem.Core.Repositorios
{
    public class BaseRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey> where TEntity : class
    {
        protected readonly SiagDbContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;

        public BaseRepository(SiagDbContext context)
        {
            _dbContext = context;
            _dbSet = _dbContext.Set<TEntity>();
        }


    }
}
