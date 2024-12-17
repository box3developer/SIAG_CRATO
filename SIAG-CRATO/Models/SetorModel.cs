using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG_CRATO.Models;

public class SetorModel
{
    [Column("id_setortrabalho")]
    public int Codigo { get; set; }

    [Column("id_deposito")]
    public int IdDeposito { get; set; }

    [Column("nm_setortrabalho")]
    public string Descricao { get; set; } = string.Empty;
}

