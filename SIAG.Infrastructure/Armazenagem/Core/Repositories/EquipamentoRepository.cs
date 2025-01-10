using Microsoft.EntityFrameworkCore;
using SIAG.CrossCutting.DTOs;
using SIAG.CrossCutting.Utils;
using SIAG.Domain.Armazenagem.Core.Interfaces;
using SIAG.Domain.Armazenagem.Core.Models;
using SIAG.Infrastructure.Configuracao;

namespace SIAG.Infrastructure.Armazenagem.Core.Repositorios
{
    public class EquipamentoRepository : BaseRepository<Equipamento, int>, IEquipamentoRepository
    {
        public EquipamentoRepository(SiagDbContext context) : base(context)
        {
        }

    }
}
