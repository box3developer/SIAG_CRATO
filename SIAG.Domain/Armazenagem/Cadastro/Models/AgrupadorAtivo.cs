using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG.Domain.Armazenagem.Cadastro.Models
{
    [Table("agrupadorativo")]
    public class AgrupadorAtivo
    {
        [Key]
        [Column("id_agrupador")]
        public Guid IdAgrupador { get; set; }

        [Column("tp_agrupamento")]
        public int TpAgrupamento { get; set; }

        [Column("codigo1")]
        public string Codigo1 { get; set; } = string.Empty;

        [Column("codigo2")]
        public string Codigo2 { get; set; } = string.Empty;

        [Column("codigo3")]
        public string Codigo3 { get; set; } = string.Empty;

        [Column("cd_sequencia")]
        public int CdSequencia { get; set; }

        [Column("dt_agrupador")]
        public DateTime DtAgrupador { get; set; }

        [Column("id_area_armazenagem")]
        public int IdAreaArmazenagem { get; set; }

        [Column("fg_status")]
        public int FgStatus { get; set; }
    }
}
