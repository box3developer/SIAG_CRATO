using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG_CRATO.Models;

public class TurnoModel
{
    [Column("cd_turno")]
    public string? CodTurno { get; set; }

    [Column("dt_inicio")]
    public DateTime DtInicio { get; set; }

    [Column("dt_fim")]
    public DateTime DtFim { get; set; }
}
