using SIAG.Application.Armazenagem.Core.DTOs;
using SIAG.CrossCutting.DTOs;
using SIAG.CrossCutting.Interfaces;
using SIAG.Domain.Armazenagem.Core.Interfaces;
using SIAG.Domain.Armazenagem.Core.Models;

namespace SIAG.Application.Armazenagem.Core.Services
{
    public class SetorTrabalhoService : BaseService<ISetorTrabalhoRepository, SetorTrabalho, SetorTrabalhoDTO>
    {
        private readonly ISetorTrabalhoRepository _repository;
        private readonly IMappingService _mappingService;

        public SetorTrabalhoService(ISetorTrabalhoRepository repository, IMappingService mappingService) : base(repository, mappingService)
        {
            _repository = repository;
            _mappingService = mappingService;
        }

        //public static async Task<List<SelectDTO<int>>> GetListSelectsAsync()
        //{
        //    //using var conexao = new SqlConnection(Global.Conexao);
        //    //var setores = await conexao.QueryAsync<SetorTrabalho>(SetorQuery.SELECT);

        //    //return setores.Select(x => new SelectDTO<int>() { Id = x.IdSetorTrabalho, Descricao = x.NmSetorTrabalho }).ToList();
        //}
    }
}
