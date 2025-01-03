using Dapper;
using Microsoft.Data.SqlClient;
using SIAG_CRATO.DTOs.EquipamentoCheckListOperador;
using SIAG_CRATO.Models;

namespace SIAG_CRATO.BLLs.EquipamentoCheckListOperador;

public class EquipamentoCheckListOperadorBLL
{
    public static async Task<bool> SetChecklistOperador(EquipamentoCheckListOperadorModel checklistOperador)
    {
        using var conexao = new SqlConnection(Global.Conexao);
        var id = await conexao.ExecuteAsync(EquipamentoCheckListOperadorQuery.INSERT, new
        {
            idEquipamento = checklistOperador.IdEquipamento,
            idOperador = checklistOperador.IdOperador,
            idChecklist = checklistOperador.IdEquipamentoChecklist,
            resposta = checklistOperador.FgResposta,
            data = checklistOperador.DtChecklist
        });

        return id > 0;
    }

    private static EquipamentoCheckListOperadorDTO ConvertToDTO(EquipamentoCheckListOperadorModel checklist)
    {
        return new()
        {
            IdEquipamento = checklist.IdEquipamento,
            IdOperador = checklist.IdOperador,
            IdEquipamentoChecklist = checklist.IdEquipamentoChecklist,
            FgResposta = checklist.FgResposta,
            DtChecklist = checklist.DtChecklist,
        };
    }
}
