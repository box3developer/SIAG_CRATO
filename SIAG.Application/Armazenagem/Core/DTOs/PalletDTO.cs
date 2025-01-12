using SIAG.Domain.Armazenagem.Core.Models;

namespace SIAG.Application.Armazenagem.Core.DTOs
{
    public class PalletDTO
    {
        public int IdPallet { get; set; }

        public int IdAreaArmazenagem { get; set; }
        public AreaArmazenagem AreaArmazenagem { get; set; }

        public Guid IdAgrupadorAtivo { get; set; }
        public AgrupadorAtivo AgrupadorAtivo { get; set; }

        public int FgStatus { get; set; }

        public int QtUtilizacao { get; set; }

        public DateTime DtUltimaMovimentacao { get; set; }

        public int CdIdentificacao { get; set; }
    }
}
