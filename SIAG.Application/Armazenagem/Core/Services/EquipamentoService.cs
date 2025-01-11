using SIAG.Application.Armazenagem.Core.DTOs;
using SIAG.CrossCutting.Interfaces;
using SIAG.Domain.Armazenagem.Core.Interfaces;
using SIAG.Domain.Armazenagem.Core.Models;

namespace SIAG.Application.Armazenagem.Core.Services
{
    public class EquipamentoService : BaseService<IEquipamentoRepository, Equipamento, EquipamentoDTO>
    {
        private readonly IEquipamentoRepository _repository;
        private readonly IMappingService _mappingService;

        public EquipamentoService(IEquipamentoRepository repository, IMappingService mappingService) : base(repository, mappingService)
        {
            _repository = repository;
            _mappingService = mappingService;
        }

    }
}
