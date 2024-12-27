using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG_CRATO.Models;

public class EquipamentoEnderecoPrioridadeModel
{
    [Column("id_equipamentoenderecoprioridade")]
    public long Codigo { get; set; }

    [Column("id_equipamentoendereco")]
    public long EnderecoId { get; set; }

    [Column("prioridade")]
    public int Prioridade { get; set; }
}
