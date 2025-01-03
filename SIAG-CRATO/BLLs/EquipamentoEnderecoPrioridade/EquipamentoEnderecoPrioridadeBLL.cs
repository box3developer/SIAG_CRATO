using Dapper;
using Microsoft.Data.SqlClient;
using SIAG_CRATO.BLLs.EquipamentoEndereco;
using SIAG_CRATO.DTOs.EquipamentoEnderecoPrioridade;
using SIAG_CRATO.Models;

namespace SIAG_CRATO.BLLs.EquipamentoEnderecoPrioridade;

public class EquipamentoEnderecoPrioridadeBLL
{
    public static async Task<List<EquipamentoEnderecoPrioridadeDTO>> GetListAsync()
    {
        using var conexao = new SqlConnection(Global.Conexao);
        var equipamentos = await conexao.QueryAsync<EquipamentoEnderecoPrioridadeModel>(EquipamentoEnderecoPrioridadeQuery.SELECT);

        return equipamentos.Select(ConvertToDTO).ToList();
    }

    private static EquipamentoEnderecoPrioridadeDTO ConvertToDTO(EquipamentoEnderecoPrioridadeModel prioridade)
    {
        return new()
        {
            IdEquipamentoEnderecoPrioridade = prioridade.IdEquipamentoEnderecoPrioridade,
            IdEquipamentoEndereco = prioridade.IdEquipamentoEndereco,
            Prioridade = prioridade.Prioridade,
        };
    }
}
