using SIAG_CRATO.Data;
using SIAG_CRATO.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG_CRATO.Models
{
    public class AgrupadorAtivoModel
    {
        [Column("id_agrupador")]
        public Guid IdAgrupador { get; set; }

        [Column("tp_agrupamento")]
        public TipoAgrupamento TipoAgrupamento { get; set; }

        [Column("codigo1")]
        public string? Codigo1 { get; set; }

        [Column("codigo2")]
        public string? Codigo2 { get; set; }

        [Column("codigo3")]
        public string? Codigo3 { get; set; }

        [Column("cd_sequencia")]
        public Int64 Sequencia { get; set; }

        [Column("dt_agrupador")]
        public DateTime? DataAgrupador { get; set; }

        [Column("id_areaarmazenagem")]
        public Int64 AreaArmazenagem { get; set; }

        [Column("fg_status")]
        public StatusAgrupador Status { get; set; }
    }
}
