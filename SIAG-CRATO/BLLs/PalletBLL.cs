using Dapper;
using Microsoft.Data.SqlClient;
using SIAG_CRATO.DTOs.Pallet;
using SIAG_CRATO.Models;

namespace SIAG_CRATO.BLLs;

public class PalletBLL
{
    const string SELECT = "SELECT id_pallet, id_areaarmazenagem, id_agrupador, fg_status, qt_utilizacao, dt_ultimamovimentacao, cd_identificacao FROM pallet WITH(NOLOCK)";
    const string INSERT = "INSERT INTO pallet (id_pallet, id_areaarmazenagem, id_agrupador, fg_status, qt_utilizacao, dt_ultimamovimentacao, cd_identificacao) VALUES (@Codigo, @AreaArmazenagem, @Agrupador, @Status, @QtUtilizacao, @DataUltimaMovimentacao, @Identificacao)";

    public async Task<List<PalletModel>> GetListAsync(FiltroPalletDTO filtro)
    {
        string query = SELECT;
        object? parametros = null;

        if (filtro != null && (filtro.Codigo > 0 || filtro.Identificacao != string.Empty))
        {
            query += " WHERE id_pallet = @idPallet or cd_identificacao = @cdPallet";
            parametros = new { idPallet = filtro.Codigo, cd_identificacao = filtro.Identificacao };
        }

        using var conexao = new SqlConnection(Global.Conexao);
        var pallets = await conexao.QueryAsync<PalletModel>(query, parametros);

        return pallets.ToList() ?? [];
    }

    public async Task<int> InsertAsync(PalletModel pallet)
    {
        var parametros = new Dictionary<string, object>
        {
            { "@Codigo", pallet.Codigo },
            { "@Status", pallet.Status },
            { "@QtdUtilizacao", pallet.QtUtilizacao },
            { "@AreaArmazenagem", pallet.AreaArmazenagemId > 0 ? pallet.AreaArmazenagemId : DBNull.Value},
            { "@Agrupador", pallet.AgrupadorId > 0 ? pallet.AgrupadorId : DBNull.Value },
            { "@DataUltimaMovimentacao", pallet.DataUltimaMovimentacao != null ? pallet.DataUltimaMovimentacao : DBNull.Value },
            { "@Identificacao", pallet.Identificacao != null ? pallet.Identificacao : DBNull.Value},
        };

        using var conexao = new SqlConnection(Global.Conexao);
        var id = await conexao.ExecuteAsync(INSERT, new DynamicParameters(parametros));

        return id;
    }
}
