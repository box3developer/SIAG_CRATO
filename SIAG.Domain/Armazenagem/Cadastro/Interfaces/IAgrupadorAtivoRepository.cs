using SIAG.CrossCutting.DTOs;
using SIAG.Domain.Armazenagem.Cadastro.Models;

namespace SIAG.Domain.Armazenagem.Cadastro.Interfaces
{
    public interface IAgrupadorAtivoRepository : IBaseRepository<AgrupadorAtivo, string>
    {
        public Task<DadosPaginadosDTO<AgrupadorAtivo>> GetListAsync(FiltroPaginacaoDTO dto);
        public Task<List<SelectDTO<string>>> GetSelectAsync(FiltroPaginacaoDTO dto);
    }
}
