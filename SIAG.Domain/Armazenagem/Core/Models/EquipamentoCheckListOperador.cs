using SIAG.Domain.Armazenagem.Attributes;
using SIAG.Domain.Armazenagem.Cadastro.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG.Domain.Armazenagem.Core.Models;

[CustomKeyEntity]
[Table("equipamentochecklistoperador")]
public class EquipamentoCheckListOperador
{
    [Key]
    [Column("id_equipamentochecklistoperador")]
    public int IdEquipamentoCheckListOperador { get; set; }

    [Column("id_equipamento")]
    public int IdEquipamento { get; set; }
    [ForeignKey(nameof(IdEquipamento))]
    public Equipamento? Equipamento { get; set; }

    [Column("id_operador")]
    public long IdOperador { get; set; }
    [ForeignKey(nameof(IdOperador))]
    public Operador? Operador { get; set; }


    [Column("id_equipamentochecklist")]
    public int IdEquipamentoChecklist { get; set; }
    [ForeignKey(nameof(IdEquipamentoChecklist))]
    public EquipamentoChecklist? EquipamentoChecklist { get; set; }

    [Column("fg_resposta")]
    public bool FgResposta { get; set; }

    [Column("dt_checklist")]
    public DateTime? DtChecklist { get; set; }
}
