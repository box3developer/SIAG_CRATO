using SIAG_CRATO.Models;
using SIAG_CRATO.Services;

namespace SIAG_CRATO.Repositories.Interfaces
{
    public interface IChamadaRepository
    {
        public Task<Guid> CriarChamadaAsync(CriarChamadaDTO dto);
    }
}
