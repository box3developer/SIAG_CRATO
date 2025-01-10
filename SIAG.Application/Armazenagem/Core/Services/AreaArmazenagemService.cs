using SIAG.Application.Armazenagem.Core.DTOs;
using SIAG.CrossCutting.Interfaces;
using SIAG.Domain.Armazenagem.Core.Interfaces;
using SIAG.Domain.Armazenagem.Core.Models;

namespace SIAG.Application.Armazenagem.Core.Services
{
    public class AreaArmazenagemService : BaseService<IAreaArmazenagemRepository, AreaArmazenagem, AreaArmazenagemDTO, long>
    {
        private readonly IAreaArmazenagemRepository _repository;
        private readonly IMappingService _mappingService;

        public AreaArmazenagemService(IAreaArmazenagemRepository repository, IMappingService mappingService) : base(repository, mappingService)
        {
            _repository = repository;
            _mappingService = mappingService;
        }

    }
}
