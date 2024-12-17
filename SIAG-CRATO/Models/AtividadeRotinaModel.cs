using System.ComponentModel.DataAnnotations.Schema;
using SIAG_CRATO.Data;

namespace SIAG_CRATO.Models;
public class AtividadeRotinaModel
{
    [Column("id_atividadeRotina")]
    public int Codigo { get; set; }

    [Column("nm_AtividadeRotina")]
    public string Nome { get; set; } = string.Empty;

    [Column("nm_procedure")]
    public string Procedure { get; set; } = string.Empty;

    [Column("fg_tipo")]
    public TipoRotina Tipo { get; set; }
}
