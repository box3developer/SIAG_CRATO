using SIAG.Application.Armazenagem.Cadastro.Services.Interfaces;
using SIAG.CrossCutting.DTOs;
using SIAG.CrossCutting.Interfaces;
using SIAG.Domain.Armazenagem.Cadastro.Interfaces;

namespace SIAG.Application.Armazenagem.Cadastro.Services
{
    public class BaseService<TRepository, TEntity, TKey, TDto> : IBaseService<TRepository, TEntity, TKey, TDto>
        where TRepository : IBaseRepository<TEntity, TKey>
        where TEntity : class
        where TKey : notnull
        where TDto : class
    {
        protected readonly TRepository _repository;
        private readonly IMappingService _mappingService;

        public BaseService(TRepository repository, IMappingService mappingService)
        {
            _repository = repository;
            _mappingService = mappingService;
        }

        public virtual async Task<bool> CreateAsync(TDto dto)
        {
            var entity = _mappingService.Map<TDto, TEntity>(dto);
            return await _repository.CreateAsync(entity);
        }

        public virtual async Task<bool> UpdateAsync(TDto dto)
        {
            var entity = _mappingService.Map<TDto, TEntity>(dto);
            return await _repository.UpdateAsync(entity);
        }

        public virtual async Task<bool> DeleteAsync(TKey id)
        {
            return await _repository.DeleteAsync(id);
        }

        public virtual async Task<TDto> GetItemAsync(TKey id)
        {
            var entity = await _repository.GetByIdAsync(id);
            var dto = _mappingService.Map<TEntity, TDto>(entity);

            return dto;
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
