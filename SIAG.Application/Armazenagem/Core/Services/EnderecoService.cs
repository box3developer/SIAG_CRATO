using SIAG.Application.Armazenagem.Core.DTOs;
using SIAG.CrossCutting.Interfaces;
using SIAG.Domain.Armazenagem.Core.Interfaces;
using SIAG.Domain.Armazenagem.Core.Models;

namespace SIAG.Application.Armazenagem.Core.Services
{
    public class EnderecoService : BaseService<IEnderecoRepository, Endereco, EnderecoDTO, int>
    {
        private readonly IEnderecoRepository _repository;
        private readonly IMappingService _mappingService;

        public EnderecoService(IEnderecoRepository repository, IMappingService mappingService) : base(repository, mappingService)
        {
            _repository = repository;
            _mappingService = mappingService;
        }

    }
}
