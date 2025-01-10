using SIAG.Application.Armazenagem.Core.DTOs;
using SIAG.CrossCutting.Interfaces;
using SIAG.Domain.Armazenagem.Core.Interfaces;
using SIAG.Domain.Armazenagem.Core.Models;

namespace SIAG.Application.Armazenagem.Core.Services
{
    public class CaixaService : BaseService<ICaixaRepository, Caixa, CaixaDTO, string>
    {
        private readonly ICaixaRepository _repository;
        private readonly IMappingService _mappingService;

        public CaixaService(ICaixaRepository repository, IMappingService mappingService) : base(repository, mappingService)
        {
            _repository = repository;
            _mappingService = mappingService;
        }

    }
}
