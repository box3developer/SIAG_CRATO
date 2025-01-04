using SIAG.CrossCutting.DTOs;
using SIAG.Domain.Armazenagem.Cadastro.Models;

namespace SIAG.Domain.Armazenagem.Cadastro.Interfaces
{
    public interface IProgramaRepository : IBaseRepository<Programa, int>
    {
        public Task<DadosPaginadosDTO<Programa>> GetListAsync(FiltroPaginacaoDTO dto);
        public Task<List<SelectDTO<int>>> GetSelectAsync(FiltroPaginacaoDTO dto);
    }
}
