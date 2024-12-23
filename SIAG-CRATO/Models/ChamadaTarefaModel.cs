using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG_CRATO.Models;

public class ChamadaTarefaModel
{
    [Column("id_tarefa")]
    public int TarefaId { get; set; }

    [Column("id_chamada")]
    public Guid ChamadaId { get; set; }

    [Column("dt_inicio")]
    public DateTime? DataInicio { get; set; }

    [Column("dt_fim")]
    public DateTime? DataFim { get; set; }
}
