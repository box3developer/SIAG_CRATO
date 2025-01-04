using SIAG.Domain.Armazenagem.Cadastro.Models;

namespace SIAG.Application.Armazenagem.Cadastro.DTOs
{
    public class PalletDTO
    {
        public int PalletId { get; set; }

        public int AreaArmazenagemId { get; set; }
        public AreaArmazenagem AreaArmazenagem { get; set; }

        public Guid AgrupadorAtivoId { get; set; }
        public AgrupadorAtivo AgrupadorAtivo { get; set; }

        public int FgStatus { get; set; }

        public int QtUtilizacao { get; set; }

        public DateTime DtUltimaMovimentacao { get; set; }

        public int CdIdentificacao { get; set; }
    }
}
