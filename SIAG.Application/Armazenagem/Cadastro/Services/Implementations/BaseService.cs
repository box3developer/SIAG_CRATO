using Microsoft.EntityFrameworkCore;
using SIAG.Application.Armazenagem.Cadastro.Services.Interfaces;
using SIAG.CrossCutting.DTOs;
using SIAG.CrossCutting.Interfaces;
using SIAG.CrossCutting.Utils;
using SIAG.Domain.Armazenagem.Cadastro.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

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
        private readonly string _keyColumn;

        public BaseService(TRepository repository, IMappingService mappingService)
        {
            _repository = repository;
            _mappingService = mappingService;
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

        public virtual async Task<DadosPaginadosDTO<TDto>> GetListAsync(FiltroPaginacaoDTO dto)
        {
            Func<IQueryable<TEntity>, IQueryable<TEntity>> includeFunc = query => query;
            Func<IQueryable<TEntity>, IQueryable<TEntity>> filterFunc = query => query;

            var dadosPaginados = await _repository.GetPaginatedAsync(
                includeFunc,
                filterFunc,
                dto.CurrentPage,
                dto.PageSize,
                dto.Impressao);

            var listaFormatada = dadosPaginados.Dados
                .Select(item => _mappingService.Map<TEntity, TDto>(item))
                .ToList();

            return new DadosPaginadosDTO<TDto>
            {
                Dados = listaFormatada,
                TotalPages = dadosPaginados.TotalPages,
                CurrentPage = dadosPaginados.CurrentPage,
                PageSize = dadosPaginados.PageSize,
                TotalRegisters = dadosPaginados.TotalRegisters
            };
        }

        public virtual async Task<List<SelectDTO<TKey>>> GetSelectAsync(FiltroPaginacaoDTO filtro)
        {
            var filtros = new Dictionary<string, Func<TEntity, string>> { };

            var resultados = await _repository.GetSelectAsync(filtro.Pesquisa, 30, filtros);

            var lista = resultados.Select(x =>
            {
                var keyValue = (TKey)typeof(TEntity).GetProperty(_keyColumn)?.GetValue(x);

                return new SelectDTO<TKey>
                {
                    Id = keyValue,
                    Descricao = $"{keyValue}"
                };
            }).ToList();

            return lista;
        }
    }
}
