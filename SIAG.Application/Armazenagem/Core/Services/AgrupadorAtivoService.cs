using SIAG.Application.Armazenagem.Core.DTOs;
using SIAG.CrossCutting.Interfaces;
using SIAG.Domain.Armazenagem.Core.Interfaces;
using SIAG.Domain.Armazenagem.Core.Models;

namespace SIAG.Application.Armazenagem.Core.Services
{
    public class AgrupadorAtivoService : BaseService<IAgrupadorAtivoRepository, AgrupadorAtivo, AgrupadorAtivoDTO>
    {
        private readonly IAgrupadorAtivoRepository _repository;
        private readonly IMappingService _mappingService;

        public AgrupadorAtivoService(IAgrupadorAtivoRepository repository, IMappingService mappingService) : base(repository, mappingService)
        {
            _repository = repository;
            _mappingService = mappingService;
        }

    }
}
