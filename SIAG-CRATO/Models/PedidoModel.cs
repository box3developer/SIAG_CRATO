using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG_CRATO.Models;

public class PedidoModel
{
    [Column("id_pedido")]
    public string? IdPedido { get; set; }

    [Column("cd_lote")]
    public string? CodigoLote { get; set; }
}
