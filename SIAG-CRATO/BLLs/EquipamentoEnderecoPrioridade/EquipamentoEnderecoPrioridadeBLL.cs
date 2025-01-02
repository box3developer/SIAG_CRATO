using Dapper;
using Microsoft.Data.SqlClient;
using SIAG_CRATO.DTOs.EquipamentoEnderecoPrioridade;
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

    private static EquipamentoEnderecoPrioridadeDTO ConvertToDTO(EquipamentoEnderecoPrioridadeModel prioridade)
    {
        return new()
        {
            Codigo = prioridade.Codigo,
            EnderecoId = prioridade.EnderecoId,
            Prioridade = prioridade.Prioridade,
        };
    }
}
