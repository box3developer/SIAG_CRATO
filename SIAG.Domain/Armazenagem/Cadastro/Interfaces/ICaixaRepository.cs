using SIAG.CrossCutting.DTOs;
using SIAG.Domain.Armazenagem.Cadastro.Models;

namespace SIAG.Domain.Armazenagem.Cadastro.Interfaces
{
    public interface ICaixaRepository : IBaseRepository<Caixa, string>
    {
        public Task<DadosPaginadosDTO<Caixa>> GetListAsync(FiltroPaginacaoDTO dto);
        public Task<List<SelectDTO<string>>> GetSelectAsync(FiltroPaginacaoDTO dto);
    }
}
