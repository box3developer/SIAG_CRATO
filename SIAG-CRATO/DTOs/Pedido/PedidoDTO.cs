namespace SIAG_CRATO.DTOs.Pedido;

public class PedidoDTO
{
    public string? IdPedido { get; set; } = string.Empty;
    public string CodigoPedido { get; set; } = string.Empty;
    public string CodigoLote { get; set; } = string.Empty;
    public string Box { get; set; } = string.Empty;
    public int QuantidadeCaixas { get; set; }
}
