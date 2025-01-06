using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SIAG.Domain.Armazenagem.Cadastro.Models
{
    [Table("Pallet")]
    public class Pallet
    {
        [Key]
        [Column("id_pallet")]
        public int IdPallet { get; set; }

        [ForeignKey("AreaArmazenagem")]
        [Column("id_area_armazenagem")]
        public int IdAreaArmazenagem { get; set; }
        public AreaArmazenagem? AreaArmazenagem { get; set; }

        [ForeignKey("AgrupadorAtivo")]
        [Column("agrupador_ativo_id")]
        public Guid AgrupadorAtivoId { get; set; }
        public AgrupadorAtivo? AgrupadorAtivo { get; set; }

        [Column("fg_status")]
        public int FgStatus { get; set; }

        [Column("qt_utilizacao")]
        public int QtUtilizacao { get; set; }

        [Column("dt_ultima_movimentacao")]
        public DateTime DtUltimaMovimentacao { get; set; }

        [Column("cd_identificacao")]
        public string CdIdentificacao { get; set; } = string.Empty;
    }
}
