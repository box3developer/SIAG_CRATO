using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG_CRATO.Models;

public class EquipamentoEnderecoModel
{
    [Column("id_equipamentoendereco")]
    public long EquipamentoEnderecoId { get; set; }

    [Column("id_equipamento")]
    public int EquipamentoId { get; set; }

    [Column("id_endereco")]
    public long EnderecoId { get; set; }
}
