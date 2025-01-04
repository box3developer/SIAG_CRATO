using SIAG.CrossCutting.DTOs;
using SIAG.Domain.Armazenagem.Cadastro.Models;

namespace SIAG.Domain.Armazenagem.Cadastro.Interfaces
{
    public interface ISetorTrabalhoRepository : IBaseRepository<SetorTrabalho, int>
    {
        public Task<DadosPaginadosDTO<SetorTrabalho>> GetListAsync(FiltroPaginacaoDTO dto);
        public Task<List<SelectDTO<int>>> GetSelectAsync(FiltroPaginacaoDTO dto);
    }
}
