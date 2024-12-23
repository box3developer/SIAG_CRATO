using Dapper;
using Microsoft.Data.SqlClient;
using SIAG_CRATO.Models;

namespace SIAG_CRATO.BLLs.EquipamentoCheckListOperador;

public class EquipamentoCheckListOperadorBLL
{
    public static async Task<bool> SetChecklistOperador(EquipamentoCheckListOperadorModel checklistOperador)
    {
        using var conexao = new SqlConnection(Global.Conexao);
        var id = await conexao.ExecuteAsync(EquipamentoCheckListOperadorQuery.INSERT, new
        {
            idEquipamento = checklistOperador.EquipamentoId,
            idOperador = checklistOperador.OperadorId,
            idChecklist = checklistOperador.ChecklistId,
            resposta = checklistOperador.Resposta,
            data = checklistOperador.Data
        });

        return id > 0;
    }
}
