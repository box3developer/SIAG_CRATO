using SIAG.Domain.Armazenagem.Core.Interfaces;
using SIAG.Domain.Armazenagem.Core.Models;
using System.Data;

namespace SIAG.Infrastructure.Armazenagem.Core.Repositorios
{
    public class DepositoRepository : BaseRepository<Deposito>, IDepositoRepository
    {
        public DepositoRepository(IDbConnection connection) : base(connection)
        {
        }

    }
}
