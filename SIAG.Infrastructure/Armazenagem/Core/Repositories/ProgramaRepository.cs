using SIAG.Domain.Armazenagem.Core.Interfaces;
using SIAG.Domain.Armazenagem.Core.Models;
using System.Data;

namespace SIAG.Infrastructure.Armazenagem.Core.Repositorios
{
    public class ProgramaRepository : BaseRepository<Programa>, IProgramaRepository
    {
        public ProgramaRepository(IDbConnection connection) : base(connection)
        {
        }

    }
}
