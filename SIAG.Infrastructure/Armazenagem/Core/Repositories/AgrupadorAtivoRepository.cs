using SIAG.Domain.Armazenagem.Core.Interfaces;
using SIAG.Domain.Armazenagem.Core.Models;
using System.Data;

namespace SIAG.Infrastructure.Armazenagem.Core.Repositorios
{
    public class AgrupadorAtivoRepository : BaseRepository<AgrupadorAtivo>, IAgrupadorAtivoRepository
    {
        public AgrupadorAtivoRepository(IDbConnection connection) : base(connection)
        {
        }
    }
}
