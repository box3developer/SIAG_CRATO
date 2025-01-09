using Dapper;
using Microsoft.Data.SqlClient;
using SIAG_CRATO.BLLs.Equipamento;
using SIAG_CRATO.DTOs.EquipamentoCheckList;
using SIAG_CRATO.Models;
namespace SIAG_CRATO.BLLs.EquipamentoChecklist;

public class EquipamentoChecklistBLL
{
    public static async Task<List<EquipamentoChecklistDTO>> GetListAsync()
    {
        using var conexao = new SqlConnection(Global.Conexao);
        var EquipamentoChecklist = await conexao.QueryAsync<EquipamentoChecklistModel>(EquipamentoChecklistQuery.SELECT);

        return EquipamentoChecklist.Select(ConvertToDTO).ToList();
    }

    public static async Task<EquipamentoChecklistDTO?> GetByIdAsync(int id)
    {
        var sql = $@"{EquipamentoChecklistQuery.SELECT} WHERE id_equipamentochecklist = @id";

        using var conexao = new SqlConnection(Global.Conexao);
        var checklist = await conexao.QueryFirstOrDefaultAsync<EquipamentoChecklistModel>(sql, new { id });

        if (checklist == null)
        {
            return null;
        }

        return ConvertToDTO(checklist);
    }

    public static async Task<List<EquipamentoChecklistDTO>> GetByModeloAsync(int id_equipamentomodelo)
    {
        var sql = $@"{EquipamentoChecklistQuery.SELECT} WHERE id_equipamentomodelo = @id_equipamentomodelo";

        using var conexao = new SqlConnection(Global.Conexao);
        var EquipamentoChecklist = await conexao.QueryAsync<EquipamentoChecklistModel>(sql, new { id_equipamentomodelo });

        return EquipamentoChecklist.Select(ConvertToDTO).ToList();
    }

    public static async Task<List<EquipamentoChecklistDTO>> GetChecklistEquipamentoByIdentificador(string nm_identificador)
    {
        var modelo = await EquipamentoBLL.GetByidentificadorAsync(nm_identificador);


        if (modelo == null || modelo.IdEquipamentoModelo == 0)
        {
            return [];
        }

        var checklists = await GetByModeloAsync(modelo.IdEquipamentoModelo);

        return checklists ?? [];
    }

    private static EquipamentoChecklistDTO ConvertToDTO(EquipamentoChecklistModel checklist)
    {
        return new()
        {
            IdEquipamentoChecklist = checklist.IdEquipamentoChecklist,
            IdEquipamentoModelo = checklist.IdEquipamentoModelo,
            NmDescricao = checklist.NmDescricao,
            FgCritico = checklist.FgCritico,
            FgStatus = checklist.FgStatus,
        };
    }
}
