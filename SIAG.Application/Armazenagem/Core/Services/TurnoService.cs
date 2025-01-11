using SIAG.Application.Armazenagem.Core.DTOs;
using SIAG.CrossCutting.Interfaces;
using SIAG.Domain.Armazenagem.Core.Interfaces;
using SIAG.Domain.Armazenagem.Core.Models;

namespace SIAG.Application.Armazenagem.Core.Services
{
    public class TurnoService : BaseService<ITurnoRepository, Turno, TurnoDTO>
    {
        private readonly ITurnoRepository _repository;
        private readonly IMappingService _mappingService;

        public TurnoService(ITurnoRepository repository, IMappingService mappingService) : base(repository, mappingService)
        {
            _repository = repository;
            _mappingService = mappingService;
        }

    }
}
