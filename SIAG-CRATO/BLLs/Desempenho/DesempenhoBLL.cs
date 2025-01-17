using Dapper;
using Microsoft.Data.SqlClient;
using SIAG_CRATO.Models;

namespace SIAG_CRATO.BLLs.Desempenho;

public class DesempenhoBLL
{
    public static async Task<List<DesempenhoModel>> GetByPerformance(long idOperador, int idSetorTrabalho, int idEquipamentoModelo)
    {
        var sql = $"{DesempenhoQuery.SELECT} WHERE id_operador = @idOperador and id_setortrabalho = @idSetorTrabalho and id_equipamentomodelo = @idEquipamentoModelo";

        using var conexao = new SqlConnection(Global.Conexao);
        var desempenhos = await conexao.QueryAsync<DesempenhoModel>(sql, new
        {
            idOperador,
            idSetorTrabalho,
            idEquipamentoModelo
        });

        return desempenhos.ToList();
    }
}
