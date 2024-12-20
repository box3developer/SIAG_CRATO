using Dapper;
using Microsoft.Data.SqlClient;
using SIAG_CRATO.Models;

namespace SIAG_CRATO.BLLs.Parametro
{
    public class ParametroBLL
    {
        public static async Task<ParametroModel?> GetParametroByParametro(string parametro)
        {
            var sql = $@"{ParametroQuery.SELECT} WHERE nm_parametro = @NmParametro";

            using var conexao = new SqlConnection(Global.Conexao);

            var equipamento = await conexao.QueryFirstOrDefaultAsync<ParametroModel>(sql, new { parametro });

            return equipamento;
        }

        public static async Task<ParametroModel?> GetParametroByTipo(string tipo)
        {
            var sql = $@"{ParametroQuery.SELECT} WHERE nm_tipo = @tipo";

            using var conexao = new SqlConnection(Global.Conexao);

            var equipamento = await conexao.QueryFirstOrDefaultAsync<ParametroModel>(sql, new { tipo });

            return equipamento;
        }
    }
}
