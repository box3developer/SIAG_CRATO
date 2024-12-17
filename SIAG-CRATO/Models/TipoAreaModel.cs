
using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG_CRATO.Models;
public class TipoAreaModel
{
    [Column("id_tipoarea")]
    public int IdTipoArea { get; set; }

    [Column("nm_tipoarea")]
    public string Descricao { get; set; } = string.Empty;
}
