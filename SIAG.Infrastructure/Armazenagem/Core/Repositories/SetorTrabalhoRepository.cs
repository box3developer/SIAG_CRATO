using SIAG.Domain.Armazenagem.Core.Interfaces;
using SIAG.Domain.Armazenagem.Core.Models;
using System.Data;

namespace SIAG.Infrastructure.Armazenagem.Core.Repositorios
{
    public class SetorTrabalhoRepository : BaseRepository<SetorTrabalho>, ISetorTrabalhoRepository
    {
        public SetorTrabalhoRepository(IDbConnection connection) : base(connection)
        {
        }

    }
}
