using Microsoft.EntityFrameworkCore;
using SIAG.CrossCutting.DTOs;
using SIAG.CrossCutting.Utils;
using SIAG.Domain.Armazenagem.Core.Interfaces;
using SIAG.Domain.Armazenagem.Core.Models;
using SIAG.Infrastructure.Configuracao;

namespace SIAG.Infrastructure.Armazenagem.Core.Repositorios
{
    public class DepositoRepository : BaseRepository<Deposito, int>, IDepositoRepository
    {
        public DepositoRepository(SiagDbContext context) : base(context)
        {
        }

    }
}
