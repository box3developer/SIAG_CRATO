namespace SIAG_CRATO.BLLs.Pedido;

public class PedidoQuery
{
    public const string SELECT = @"SELECT id_pedido, cd_pedido, cd_lote, cd_box FROM pedido WITH(NOLOCK)";
}
