using SIAG.Domain.Armazenagem.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG.Domain.Armazenagem.Cadastro.Models;

[CustomKeyEntity]
[Table("equipamentochecklist")]
public class EquipamentoChecklist
{
    [Key]
    [Column("id_equipamentochecklist")]
    public int IdEquipamentoChecklist { get; set; }

    [Column("id_equipamento")]
    public int IdEquipamentoModelo { get; set; }
    [ForeignKey(nameof(IdEquipamentoModelo))]
    public EquipamentoModelo? EquipamentoModelo { get; set; }

    [Column("nm_descricao")]
    public string? NmDescricao { get; set; }

    [Column("fg_critico")]
    public bool? FgCritico { get; set; }

    [Column("fg_status")]
    public int? FgStatus { get; set; }
}
