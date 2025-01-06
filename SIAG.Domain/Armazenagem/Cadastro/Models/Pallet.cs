using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SIAG.Domain.Armazenagem.Cadastro.Models
{
    [Table("pallet")]
    public class Pallet
    {
        [Key]
        [Column("id_pallet")]
        public int IdPallet { get; set; }

        [ForeignKey("areaArmazenagem")]
        [Column("id_areaarmazenagem")]
        public int IdAreaArmazenagem { get; set; }
        public AreaArmazenagem? AreaArmazenagem { get; set; }

        [ForeignKey("agrupadorativo")]
        [Column("id_agrupadorativo")]
        public Guid IdAgrupadorAtivo { get; set; }
        public AgrupadorAtivo? AgrupadorAtivo { get; set; }

        [Column("fg_status")]
        public int FgStatus { get; set; }

        [Column("qt_utilizacao")]
        public int QtUtilizacao { get; set; }

        [Column("dt_ultimamovimentacao")]
        public DateTime DtUltimaMovimentacao { get; set; }

        [Column("cd_identificacao")]
        public string CdIdentificacao { get; set; } = string.Empty;
    }
}
