using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG_CRATO.Models;

public class EquipamentoCheckListOperadorModel
{
    [Column("id_equipamento")]
    public int EquipamentoId { get; set; }

    [Column("id_operador")]
    public long OperadorId { get; set; }

    [Column("id_equipamentochecklist")]
    public int ChecklistId { get; set; }

    [Column("fg_resposta")]
    public bool Resposta { get; set; }

    [Column("dt_checklist")]
    public DateTime? Data { get; set; }
}
