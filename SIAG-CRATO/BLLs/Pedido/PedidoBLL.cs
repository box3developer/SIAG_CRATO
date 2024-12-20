using Dapper;
using Microsoft.Data.SqlClient;
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
}
