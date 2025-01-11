using SIAG.CrossCutting.Interfaces;
using SIAG.Domain.Armazenagem.Core.Interfaces;

namespace SIAG.Application.Armazenagem.Core.Services
{
    public class BaseService<TRepository, TEntity, TDto>
        where TRepository : IBaseRepository<TEntity>
        where TEntity : class
        where TDto : class
    {
        protected readonly TRepository _repository;
        private readonly IMappingService _mappingService;

        public BaseService(TRepository repository, IMappingService mappingService)
        {
            _repository = repository;
            _mappingService = mappingService;
        }

    }
}
