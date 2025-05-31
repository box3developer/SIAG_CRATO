using Dapper;
using Microsoft.Data.SqlClient;

namespace SIAG_CRATO.BLLs.OperadorHistorico;

public class OperadorHistoricoBLL
{
    public static async Task<bool> SetHistorico(long id_operador, int id_equipamento, int evento)
    {
        using var conexao = new SqlConnection(Global.Conexao);
        var result = await conexao.ExecuteAsync(OperadorHistoricoQuery.INSERT, new
        {
            id_operador,
            evento,
            id_equipamento
        });

        return result > 0;
    }
}
