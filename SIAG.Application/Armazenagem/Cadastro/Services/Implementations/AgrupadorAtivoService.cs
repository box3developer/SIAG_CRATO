using SIAG.CrossCutting.DTOs;
using SIAG.CrossCutting.Interfaces;
using SIAG.Domain.Armazenagem.Cadastro.Interfaces;

namespace SIAG.Application.Armazenagem.Cadastro.Services.Implementations
{
    public class AgrupadorAtivoService<TRepository, TEntity, TKey, TDto> : BaseService<TRepository, TEntity, TKey, TDto>
        where TRepository : IBaseRepository<TEntity, TKey>
        where TEntity : class
        where TKey : notnull
        where TDto : class
    {
        private readonly TRepository _repository;
        private readonly IMappingService _mappingService;

        public AgrupadorAtivoService(TRepository repository, IMappingService mappingService) : base(repository, mappingService)
        {
            _repository = repository;
            _mappingService = mappingService; 
        }

        public async Task<DadosPaginadosDTO<TDto>> GetListAsync(FiltroPaginacaoDTO dto)
        {
            var lista = await _repository.GetListAsync(dto);

            var listaFormatada = lista.Dados.Select(x => _mappingService.Map<TEntity, TDto>(x)).ToList();

            return new DadosPaginadosDTO<TDto>
            {
                Dados = listaFormatada,
                TotalPages = lista.TotalPages,
                CurrentPage = lista.CurrentPage,
                PageSize = lista.PageSize,
                TotalRegisters = lista.TotalRegisters
            };
        }

        public async Task<List<SelectDTO<TKey>>> GetSelectAsync(FiltroPaginacaoDTO filtro)
        {
            var lista = await _repository.GetSelectAsync(filtro);

            return lista;
        }
    }
}