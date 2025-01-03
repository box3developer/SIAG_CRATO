using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SIAG.Domain.Armazenagem.Cadastro.Models
{
    public class Pallet
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
