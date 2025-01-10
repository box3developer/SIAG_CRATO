using SIAG.Application.Armazenagem.Core.DTOs;
using SIAG.CrossCutting.Interfaces;
using SIAG.Domain.Armazenagem.Core.Interfaces;
using SIAG.Domain.Armazenagem.Core.Models;

namespace SIAG.Application.Armazenagem.Core.Services
{
    public class RegiaoTrabalhoService : BaseService<IRegiaoTrabalhoRepository, RegiaoTrabalho, RegiaoTrabalhoDTO, int>
    {
        private readonly IRegiaoTrabalhoRepository _repository;
        private readonly IMappingService _mappingService;

        public RegiaoTrabalhoService(IRegiaoTrabalhoRepository repository, IMappingService mappingService) : base(repository, mappingService)
        {
            _repository = repository;
            _mappingService = mappingService;
        }

    }
}
