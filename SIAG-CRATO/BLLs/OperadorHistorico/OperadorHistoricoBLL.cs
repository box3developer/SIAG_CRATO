using Dapper;
using Microsoft.Data.SqlClient;

namespace SIAG_CRATO.BLLs.OperadorHistorico
{
    public class OperadorHistoricoBLL
    {
        public static async Task<bool> SetHistorico(int id_operador, int id_equipamento)
        {
            using var conexao = new SqlConnection(Global.Conexao);

            var sql = $@"{OperadorHistoricoQuery.INSERT}";

            var result = await conexao.ExecuteAsync(sql, new { id_operador,evento = 1, id_equipamento });
            return result > 0;
        }
    }
}
