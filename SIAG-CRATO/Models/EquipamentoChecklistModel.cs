using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG_CRATO.Models
{
    public class EquipamentoChecklistModel
    {
        [Column("id_equipamentochecklist")]
        public int IdEquipamentoChecklist { get; set; }

        [Column("id_equipamentomodelo")]
        public int IdEquipamentoModelo { get; set; }

        [Column("nm_descricao")]
        public string? NmDescricao { get; set; }

        [Column("fg_critico")]
        public bool? FgCritico { get; set; }

        [Column("fg_status")]
        public int? FgStatus { get; set; }
    }
}
