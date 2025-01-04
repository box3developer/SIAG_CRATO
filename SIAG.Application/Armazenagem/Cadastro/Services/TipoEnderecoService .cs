using SIAG.CrossCutting.DTOs;
using SIAG.CrossCutting.Interfaces;
using SIAG.Domain.Armazenagem.Cadastro.Interfaces;
using SIAG.Domain.Armazenagem.Cadastro.Models;

namespace SIAG.Application.Armazenagem.Cadastro.Services
{
    public class TipoEnderecoService : BaseService<ITipoEnderecoRepository, TipoEndereco, TipoEnderecoDTO, int>
    {
        private readonly ITipoEnderecoRepository _repository;
        private readonly IMappingService _mappingService;

        public TipoEnderecoService(ITipoEnderecoRepository repository, IMappingService mappingService) : base(repository, mappingService)
        {
            _repository = repository;
            _mappingService = mappingService;
        }

        public async Task<DadosPaginadosDTO<TipoEnderecoDTO>> GetListAsync(FiltroPaginacaoDTO dto)
        {
            var lista = await _repository.GetListAsync(dto);

            var listaFormatada = lista.Dados.Select(x => _mappingService.Map<TipoEndereco, TipoEnderecoDTO>(x)).ToList();

            return new DadosPaginadosDTO<TipoEnderecoDTO>
            {
                Dados = listaFormatada,
                TotalPages = lista.TotalPages,
                CurrentPage = lista.CurrentPage,
                PageSize = lista.PageSize,
                TotalRegisters = lista.TotalRegisters
            };
        }

        public async Task<List<SelectDTO<int>>> GetSelectAsync(FiltroPaginacaoDTO filtro)
        {
            var lista = await _repository.GetSelectAsync(filtro);

            return lista;
        }
    }
}