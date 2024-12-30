using Dapper;
using Microsoft.Data.SqlClient;
using SIAG_CRATO.Models;

namespace SIAG_CRATO.BLLs.EquipamentoEndereco;

public class EquipamentoEnderecoPrioridadeBLL
{
    public static async Task<List<EquipamentoEnderecoPrioridadeModel>> GetListAsync()
    {
        using var conexao = new SqlConnection(Global.Conexao);
        var equipamentos = await conexao.QueryAsync<EquipamentoEnderecoPrioridadeModel>(EquipamentoEnderecoPrioridadeQuery.SELECT);

        return equipamentos.ToList();
    }
}
