using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG_CRATO.Models
{
    public class EquipamentoManutencaoModel
    {
        [Column("id_equipamento_manutencao")]
        public int Id { get; set; }

        [Column("id_equipamento")]
        public int IdEquipamento { get; set; }

        [Column("fg_tipo_manutencao")]
        public int TipoManutencao { get; set; }

        [Column("dt_inicio")]
        public int DtInicio { get; set; }

        [Column("dt_fim")]
        public int DtFim { get; set; }
    }
}
