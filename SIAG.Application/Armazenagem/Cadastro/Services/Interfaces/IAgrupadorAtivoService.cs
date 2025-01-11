using SIAG.Domain.Armazenagem.Cadastro.Interfaces;

namespace SIAG.Application.Armazenagem.Cadastro.Services.Interfaces
{
    public interface IAgrupadorAtivoService<TRepository, TEntity, TKey, TDTO>
    : IBaseService<TRepository, TEntity, TKey, TDTO>
        where TRepository : IBaseRepository<TEntity, TKey>
        where TEntity : class
        where TDTO : class
    {
    }

}