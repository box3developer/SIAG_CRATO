using Dapper;
using Microsoft.Data.SqlClient;

namespace SIAG_CRATO.BLLs.ChamadaPendencia;

public class ChamadaPendenciaBLL
{
    public static async Task<bool> SetChamadaPai(Guid idChamada, Guid idChamadaPai)
    {
        using var conexao = new SqlConnection(Global.Conexao);
        var id = await conexao.ExecuteAsync(ChamadaPendenciaQuery.UPDATE_CHAMADA_PAI, new
        {
            idChamadaPai,
            idChamada
        });

        return id > 0;
    }

    public static async Task<bool> SetChamadaOrigem(Guid idChamada, Guid idChamadaOrigem)
    {
        using var conexao = new SqlConnection(Global.Conexao);
        var id = await conexao.ExecuteAsync(ChamadaPendenciaQuery.UPDATE_CHAMADA_ORIGEM, new
        {
            idChamadaOrigem,
            idChamada
        });

        return id > 0;
    }
}
