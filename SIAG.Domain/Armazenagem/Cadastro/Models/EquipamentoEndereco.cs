using SIAG.Domain.Armazenagem.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG.Domain.Armazenagem.Cadastro.Models;

[CustomKeyEntity]
[Table("equipamentoendereco")]
public class EquipamentoEndereco
{
    [Key]
    [Column("id_equipamentoendereco")]
    public long IdEquipamentoEndereco { get; set; }

    [Column("id_equipamento")]
    public int IdEquipamento { get; set; }

    [ForeignKey(nameof(IdEquipamento))]
    public Equipamento? Equipamento { get; set; }

    [Column("id_endereco")]
    public long IdEndereco { get; set; }
}
