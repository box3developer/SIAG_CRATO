using SIAG.CrossCutting.DTOs;
using SIAG.Domain.Armazenagem.Cadastro.Models;

namespace SIAG.Domain.Armazenagem.Cadastro.Interfaces
{
    public interface IPalletRepository : IBaseRepository<Pallet, int>
    {
        public Task<DadosPaginadosDTO<Pallet>> GetListAsync(FiltroPaginacaoDTO dto);
        public Task<List<SelectDTO<int>>> GetSelectAsync(FiltroPaginacaoDTO dto);
    }
}
