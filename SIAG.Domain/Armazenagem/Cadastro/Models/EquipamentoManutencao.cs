using SIAG.Domain.Armazenagem.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG.Domain.Armazenagem.Cadastro.Models;

[CustomKeyEntity]
[Table("equipamentomanutencao")]
public class EquipamentoManutencao
{
    [Key]
    [Column("id_equipamentomanutencao")]
    public int IdEquipamentoManutencao { get; set; }

    [Column("id_equipamento")]
    public int IdEquipamento { get; set; }
    [ForeignKey(nameof(IdEquipamento))]
    public Equipamento? Equipamento { get; set; }

    [Column("fg_tipo_manutencao")]
    public int FgTipoManutencao { get; set; }

    [Column("dt_inicio")]
    public DateTime DtInicio { get; set; }

    [Column("dt_fim")]
    public DateTime DtFim { get; set; }
}
