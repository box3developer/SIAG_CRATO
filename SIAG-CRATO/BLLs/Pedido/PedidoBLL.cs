using Dapper;
using Microsoft.Data.SqlClient;
using SIAG_CRATO.BLLs.Caixa;
using SIAG_CRATO.DTOs.Caixa;
using SIAG_CRATO.DTOs.Pedido;
using SIAG_CRATO.Models;

namespace SIAG_CRATO.BLLs.Pedido;

public class PedidoBLL
{
    public static async Task<PedidoModel?> GetById(string id)
    {
        var sql = $"{PedidoQuery.SELECT} WHERE id_pedido = @id";

        using var conexao = new SqlConnection(Global.Conexao);
        var pedido = await conexao.QueryFirstOrDefaultAsync<PedidoModel>(sql, new { id });

        return pedido;
    }

    public static async Task<PedidoDTO?> GetByDTO(FiltroCaixaPedidoDTO dto)
    {
        var sql = $"{PedidoQuery.SELECT} WHERE id_pedido = @idPedido";

        using var conexao = new SqlConnection(Global.Conexao);
        var pedido = await conexao.QueryFirstOrDefaultAsync<PedidoModel>(sql, new { idPedido = dto.IdPedido });

        if (pedido == null)
        {
            return null;
        }

        return new PedidoDTO()
        {
            IdPedido = pedido.IdPedido,
            CodigoPedido = pedido.CodigoPedido ?? "",
            CodigoLote = pedido.CodigoLote ?? "",
            Box = pedido.Box ?? "",
            QuantidadeCaixas = await CaixaBLL.GetQuantidadeByPedido(dto.IdPedido, dto.CodigoPedido, dto.IdPallet)
        };
    }
}
