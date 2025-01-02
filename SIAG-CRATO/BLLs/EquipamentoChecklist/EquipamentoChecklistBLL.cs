using Dapper;
using Microsoft.Data.SqlClient;
using SIAG_CRATO.BLLs.Equipamento;
using SIAG_CRATO.DTOs.EquipamentoCheckList;
using SIAG_CRATO.Models;
namespace SIAG_CRATO.BLLs.EquipamentoChecklist;

public class EquipamentoChecklistBLL
{
    public static async Task<List<EquipamentoChecklistModel>> GetListAsync()
    {
        using var conexao = new SqlConnection(Global.Conexao);
        var EquipamentoChecklist = await conexao.QueryAsync<EquipamentoChecklistModel>(EquipamentoChecklistQuery.SELECT);

        return EquipamentoChecklist.ToList();
    }

    public static async Task<EquipamentoChecklistModel?> GetByIdAsync(int id)
    {
        var sql = $@"{EquipamentoChecklistQuery.SELECT} WHERE id_equipamentochecklist = @id";

        using var conexao = new SqlConnection(Global.Conexao);
        var EquipamentoChecklist = await conexao.QueryFirstOrDefaultAsync<EquipamentoChecklistModel>(sql, new { id });

        return EquipamentoChecklist;
    }

    public static async Task<List<EquipamentoChecklistModel>> GetByModeloAsync(int id_equipamentomodelo)
    {
        var sql = $@"{EquipamentoChecklistQuery.SELECT}WHERE id_equipamentomodelo = @id_equipamentomodelo";

        using var conexao = new SqlConnection(Global.Conexao);
        var EquipamentoChecklist = await conexao.QueryAsync<EquipamentoChecklistModel>(sql, new { id_equipamentomodelo });

        return EquipamentoChecklist.ToList();
    }

    public static async Task<List<EquipamentoChecklistModel>> GetChecklistEquipamentoByIdentificador(string nm_identificador)
    {
        var modelo = await EquipamentoBLL.GetByidentificadorAsync(nm_identificador);


        if (modelo == null || modelo.ModeloId == 0)
        {
            return [];
        }

        var checklists = await GetByModeloAsync(modelo.ModeloId);

        return checklists ?? [];
    }

    private static EquipamentoChecklistDTO ConvertToDTO(EquipamentoChecklistModel checklist)
    {
        return new()
        {
            EquipamentoChecklistId = checklist.IdEquipamentoChecklist,
            EquipamentoModeloId = checklist.IdEquipamentoModelo,
            NomeDescricao = checklist.NmDescricao,
            Critico = checklist.FgCritico,
            Status = checklist.FgStatus,
        };
    }
}
