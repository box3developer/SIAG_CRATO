using SIAG.Domain.Armazenagem.Core.Interfaces;
using SIAG.Domain.Armazenagem.Core.Models;
using System.Data;

namespace SIAG.Infrastructure.Armazenagem.Core.Repositorios
{
    public class CaixaRepository : BaseRepository<Caixa>, ICaixaRepository
    {
        public CaixaRepository(IDbConnection connection) : base(connection)
        {
        }

    }
}
