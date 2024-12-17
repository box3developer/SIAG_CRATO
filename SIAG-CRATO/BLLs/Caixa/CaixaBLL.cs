using Dapper;
using Microsoft.Data.SqlClient;
using SIAG_CRATO.Models;

namespace SIAG_CRATO.BLLs.Caixa
{
    public class CaixaBLL
    {
        public async static Task<CaixaModel?> GetCaixaByPalletId(int id_pallet)
        {
            var sql = $@"{CaixaQuery.SELECT} WHERE id_pallet = @id_pallet";

            using var conexao = new SqlConnection(Global.Conexao);

            var caixa = await conexao.QueryFirstOrDefaultAsync<CaixaModel>(sql, new { id_pallet });

            return caixa;
        }
    }
}
