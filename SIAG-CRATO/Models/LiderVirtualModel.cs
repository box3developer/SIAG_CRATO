using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG_CRATO.Models;

public class LiderVirtualModel
{
    [Column("id_lidervirtual")]
    public string? IdLiderVirtual { get; set; }

    [Column("id_operador")]
    public string? IdOperador { get; set; }

    [Column("id_equipamentoorigem")]
    public string? IdEquipamentoOrigem { get; set; }

    [Column("id_equipamentodestino")]
    public string? IdEquipamentoDestino { get; set; }

    [Column("dt_login")]
    public DateTime? DataLogin { get; set; }

    [Column("dt_logoff")]
    public DateTime? DataLogoff { get; set; }

    [Column("id_operadorlogin")]
    public string? IdOperadorLogin { get; set; }

    [Column("dt_loginlimite")]
    public DateTime? DataLoginLimite { get; set; }
}
