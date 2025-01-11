using SIAG.Domain.Armazenagem.Core.Interfaces;
using SIAG.Domain.Armazenagem.Core.Models;
using System.Data;

namespace SIAG.Infrastructure.Armazenagem.Core.Repositorios
{
    public class TurnoRepository : BaseRepository<Turno>, ITurnoRepository
    {
        public TurnoRepository(IDbConnection connection) : base(connection)
        {
        }
    }
}
