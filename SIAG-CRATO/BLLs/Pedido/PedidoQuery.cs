namespace SIAG_CRATO.BLLs.Pedido;

public class PedidoQuery
{
    public const string SELECT = @"SELECT id_pedido, cd_lote FROM pedido WITH(NOLOCK)";
}
