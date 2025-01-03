using Dapper;
using Microsoft.Data.SqlClient;
using SIAG_CRATO.DTOs.Pedido;
using SIAG_CRATO.Models;

namespace SIAG_CRATO.BLLs.Pedido;

public class PedidoBLL
{
    public static async Task<PedidoDTO?> GetById(string id)
    {
        var sql = $"{PedidoQuery.SELECT} WHERE id_pedido = @id";

        using var conexao = new SqlConnection(Global.Conexao);
        var pedido = await conexao.QueryFirstOrDefaultAsync<PedidoModel>(sql, new { id });

        if (pedido == null)
        {
            return null;
        }

        return ConvertToDTO(pedido);
    }

    private static PedidoDTO ConvertToDTO(PedidoModel pedido)
    {
        return new()
        {
            IdPedido = pedido.IdPedido,
            CdPedido = pedido.CdPedido ?? "",
            CdLote = pedido.CdLote ?? "",
            CdBox = pedido.CdBox ?? "",
            NrCaixas = 0,
        };
    }
}
