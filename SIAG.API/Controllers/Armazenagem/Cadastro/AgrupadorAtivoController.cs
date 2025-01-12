using Microsoft.AspNetCore.Mvc;
using SIAG.Application.Armazenagem.Cadastro.Services.Interfaces;
using SIAG.Domain.Armazenagem.Cadastro.Interfaces;

namespace SIAG.API.Controllers.Armazenagem.Cadastro
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgrupadorAtivoController<
        TService,
        TRepository,
        TEntity,
        TKey,
        TDTO
    > : BaseController<
            IBaseService<TRepository, TEntity, TKey, TDTO>,
            TRepository,
            TEntity,
            TKey,
            TDTO
        >
        where TService : IBaseService<TRepository, TEntity, TKey, TDTO>
        where TRepository : IBaseRepository<TEntity, TKey>
        where TEntity : class
        where TDTO : class
    {
        public AgrupadorAtivoController(
            IBaseService<TRepository, TEntity, TKey, TDTO> service
        ) : base(service)
        {
        }
    }
}
