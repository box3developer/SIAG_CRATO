using SIAG.Domain.Armazenagem.Core.Interfaces;
using SIAG.Domain.Armazenagem.Core.Models;
using System.Data;

namespace SIAG.Infrastructure.Armazenagem.Core.Repositorios
{
    public class EnderecoRepository : BaseRepository<Endereco>, IEnderecoRepository
    {
        public EnderecoRepository(IDbConnection connection) : base(connection)
        {
        }

    }
}
