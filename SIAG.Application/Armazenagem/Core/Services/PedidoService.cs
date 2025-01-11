using SIAG.Application.Armazenagem.Core.DTOs;
using SIAG.CrossCutting.Interfaces;
using SIAG.Domain.Armazenagem.Core.Interfaces;
using SIAG.Domain.Armazenagem.Core.Models;

namespace SIAG.Application.Armazenagem.Core.Services
{
    public class PedidoService : BaseService<IPedidoRepository, Pedido, PedidoDTO>
    {
        private readonly IPedidoRepository _repository;
        private readonly IMappingService _mappingService;

        public PedidoService(IPedidoRepository repository, IMappingService mappingService) : base(repository, mappingService)
        {
            _repository = repository;
            _mappingService = mappingService;
        }

    }
}
