using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SIAG.Domain.Armazenagem.Cadastro.Models
{
    public class Pallet
    {
        [Key]
        public int PalletId { get; set; }

        [ForeignKey("AreaArmazenagemModel")]
        public int AreaArmazenagemId { get; set; }
        public AreaArmazenagem AreaArmazenagem { get; set; }

        [ForeignKey("AgrupadorAtivoModel")]
        public Guid AgrupadorAtivoId { get; set; }
        public AgrupadorAtivo AgrupadorAtivo { get; set; }

        public int FgStatus { get; set; }

        public int QtUtilizacao { get; set; }

        public DateTime DtUltimaMovimentacao { get; set; }

        public string CdIdentificacao { get; set; }
    }
}
