using SIAG.Domain.Armazenagem.Core.Interfaces;
using SIAG.Domain.Armazenagem.Core.Models;
using System.Data;

namespace SIAG.Infrastructure.Armazenagem.Core.Repositorios
{
    public class RegiaoTrabalhoRepository : BaseRepository<RegiaoTrabalho>, IRegiaoTrabalhoRepository
    {
        public RegiaoTrabalhoRepository(IDbConnection connection) : base(connection)
        {
        }
    }
}
