using SIAG.Domain.Armazenagem.Core.Interfaces;
using SIAG.Domain.Armazenagem.Core.Models;
using System.Data;

namespace SIAG.Infrastructure.Armazenagem.Core.Repositorios
{
    public class PalletRepository : BaseRepository<Pallet>, IPalletRepository
    {
        public PalletRepository(IDbConnection connection) : base(connection)
        {
        }

    }
}
