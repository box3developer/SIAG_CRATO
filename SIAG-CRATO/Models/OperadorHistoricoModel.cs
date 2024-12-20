using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG_CRATO.Models
{
    public class OperadorHistoricoModel
    {
        [Column("id_operador")]
        public int? IdOperador { get; set;}

        [Column("id_equipamento")]
        public int? IdEquipamento { get; set; }

        [Column("cd_evento")]
        public int? CdEvento { get; set; }

        [Column("dt_evento")]
        public DateTime? DtEvento { get; set; }

    }
}
