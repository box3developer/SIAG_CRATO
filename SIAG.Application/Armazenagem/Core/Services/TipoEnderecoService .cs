using SIAG.Application.Armazenagem.Core.DTOs;
using SIAG.CrossCutting.Interfaces;
using SIAG.Domain.Armazenagem.Core.Interfaces;
using SIAG.Domain.Armazenagem.Core.Models;

namespace SIAG.Application.Armazenagem.Core.Services
{
    public class TipoEnderecoService : BaseService<ITipoEnderecoRepository, TipoEndereco, TipoEnderecoDTO>
    {
        private readonly ITipoEnderecoRepository _repository;
        private readonly IMappingService _mappingService;

        public TipoEnderecoService(ITipoEnderecoRepository repository, IMappingService mappingService) : base(repository, mappingService)
        {
            _repository = repository;
            _mappingService = mappingService;
        }

    }
}
