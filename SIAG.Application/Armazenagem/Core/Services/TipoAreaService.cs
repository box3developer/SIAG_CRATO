using SIAG.Application.Armazenagem.Core.DTOs;
using SIAG.CrossCutting.Interfaces;
using SIAG.Domain.Armazenagem.Core.Interfaces;
using SIAG.Domain.Armazenagem.Core.Models;

namespace SIAG.Application.Armazenagem.Core.Services
{
    public class TipoAreaService : BaseService<ITipoAreaRepository, TipoArea, TipoAreaDTO, int>
    {
        private readonly ITipoAreaRepository _repository;
        private readonly IMappingService _mappingService;

        public TipoAreaService(ITipoAreaRepository repository, IMappingService mappingService) : base(repository, mappingService)
        {
            _repository = repository;
            _mappingService = mappingService;
        }

    }
}
