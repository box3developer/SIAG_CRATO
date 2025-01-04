using SIAG.CrossCutting.DTOs;
using SIAG.Domain.Armazenagem.Cadastro.Models;

namespace SIAG.Domain.Armazenagem.Cadastro.Interfaces
{
    public interface ITipoAreaRepository : IBaseRepository<TipoArea, int>
    {
        public Task<DadosPaginadosDTO<TipoArea>> GetListAsync(FiltroPaginacaoDTO dto);
        public Task<List<SelectDTO<int>>> GetSelectAsync(FiltroPaginacaoDTO dto);
    }
}
