using SIAG.Domain.Armazenagem.Core.Interfaces;
using SIAG.Domain.Armazenagem.Core.Models;
using System.Data;

namespace SIAG.Infrastructure.Armazenagem.Core.Repositorios
{
    public class AreaArmazenagemRepository : BaseRepository<AreaArmazenagem>, IAreaArmazenagemRepository
    {
        public AreaArmazenagemRepository(IDbConnection connection) : base(connection)
        {
        }

    }
}
