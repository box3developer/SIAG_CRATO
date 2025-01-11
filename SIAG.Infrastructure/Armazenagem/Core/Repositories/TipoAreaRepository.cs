using SIAG.Domain.Armazenagem.Core.Interfaces;
using SIAG.Domain.Armazenagem.Core.Models;
using System.Data;

namespace SIAG.Infrastructure.Armazenagem.Core.Repositorios
{
    public class TipoAreaRepository : BaseRepository<TipoArea>, ITipoAreaRepository
    {
        public TipoAreaRepository(IDbConnection connection) : base(connection)
        {
        }

    }
}
