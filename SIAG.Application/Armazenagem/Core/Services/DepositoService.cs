using SIAG.Application.Armazenagem.Core.DTOs;
using SIAG.CrossCutting.Interfaces;
using SIAG.Domain.Armazenagem.Core.Interfaces;
using SIAG.Domain.Armazenagem.Core.Models;

namespace SIAG.Application.Armazenagem.Core.Services
{
    public class DepositoService : BaseService<IDepositoRepository, Deposito, DepositoDTO, int>
    {
        private readonly IDepositoRepository _repository;
        private readonly IMappingService _mappingService;

        public DepositoService(IDepositoRepository repository, IMappingService mappingService) : base(repository, mappingService)
        {
            _repository = repository;
            _mappingService = mappingService;
        }

    }
}
