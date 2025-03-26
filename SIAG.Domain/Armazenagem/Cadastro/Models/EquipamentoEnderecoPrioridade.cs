using SIAG.Domain.Armazenagem.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG.Domain.Armazenagem.Cadastro.Models;

[CustomKeyEntity]
[Table("equipamentoenderecoprioridade")]
public class EquipamentoEnderecoPrioridade
{
    [Key]
    [Column("id_equipamentoenderecoprioridade")]
    public long IdEquipamentoEnderecoPrioridade { get; set; }

    [Column("id_equipamentoendereco")]
    public long IdEquipamentoEndereco { get; set; }

    [Column("prioridade")]
    public int Prioridade { get; set; }
}
