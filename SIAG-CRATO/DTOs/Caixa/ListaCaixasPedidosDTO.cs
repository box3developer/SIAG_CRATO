using SIAG_CRATO.DTOs.Pedido;

namespace SIAG_CRATO.DTOs.Caixa;

public class ListaCaixasPedidosDTO
{
    public List<CaixaPedidoDTO> Caixas { get; set; } = [];
    public List<PedidoDTO> Pedidos { get; set; } = [];
}
