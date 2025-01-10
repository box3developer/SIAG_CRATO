using SIAG.Application.Armazenagem.Core.DTOs;
using SIAG.CrossCutting.Interfaces;
using SIAG.Domain.Armazenagem.Core.Interfaces;
using SIAG.Domain.Armazenagem.Core.Models;

namespace SIAG.Application.Armazenagem.Core.Services
{
    public class ProgramaService : BaseService<IProgramaRepository, Programa, ProgramaDTO, int>
    {
        private readonly IProgramaRepository _repository;
        private readonly IMappingService _mappingService;

        public ProgramaService(IProgramaRepository repository, IMappingService mappingService) : base(repository, mappingService)
        {
            _repository = repository;
            _mappingService = mappingService;
        }

    }
}
