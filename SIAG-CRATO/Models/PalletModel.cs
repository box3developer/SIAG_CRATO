using System.ComponentModel.DataAnnotations.Schema;
using SIAG_CRATO.Data;

namespace SIAG_CRATO.Models;
public class PalletModel
{
    [Column("id_pallet")]
    public int Codigo { get; set; }

    [Column("cd_identificacao")]
    public string Identificacao { get; set; } = string.Empty;

    [Column("qt_utilizacao")]
    public int QtUtilizacao { get; set; }

    [Column("id_areaarmazenagem")]
    public Guid AreaArmazenagemId { get; set; }

    [Column("id_agrupador")]
    public Guid AgrupadorId { get; set; }

    [Column("fg_status")]
    public StatusPallet Status { get; set; }

    [Column("dt_ultimamovimentacao")]
    public DateTime? DataUltimaMovimentacao { get; set; }
}
