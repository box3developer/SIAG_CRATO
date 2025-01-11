using SIAG.Domain.Armazenagem.Core.Interfaces;
using SIAG.Domain.Armazenagem.Core.Models;
using System.Data;

namespace SIAG.Infrastructure.Armazenagem.Core.Repositorios
{
    public class TipoEnderecoRepository : BaseRepository<TipoEndereco>, ITipoEnderecoRepository
    {
        public TipoEnderecoRepository(IDbConnection connection) : base(connection)
        {
        }

    }
}
