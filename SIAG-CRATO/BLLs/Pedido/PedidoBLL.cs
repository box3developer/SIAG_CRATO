using Dapper;
using Microsoft.Data.SqlClient;
using SIAG_CRATO.DTOs.Pedido;
using SIAG_CRATO.Models;

namespace SIAG_CRATO.BLLs.Pedido;

public class PedidoBLL
{
    public static async Task<PedidoDTO?> GetById(string id)
    {
        try
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
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        
    }

    private static PedidoDTO ConvertToDTO(PedidoModel pedido)
    {
        return new()
        {
            IdPedido = long.Parse(pedido.IdPedido ?? ""),
            CdPedido = int.Parse(pedido.CdPedido??""),
            CdLote = int.Parse(pedido.CdLote ?? ""),
            CdBox = int.Parse(pedido.CdBox ?? ""),
            NrCaixas = 0,
        };
    }
}
