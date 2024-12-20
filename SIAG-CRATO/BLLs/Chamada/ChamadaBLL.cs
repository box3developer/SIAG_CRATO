using Dapper;
using Microsoft.Data.SqlClient;
using SIAG_CRATO.Data;
using SIAG_CRATO.DTOs.Chamada;
using SIAG_CRATO.Models;

namespace SIAG_CRATO.BLLs.Chamada;

public class ChamadaBLL
{
    public static async Task<ChamadaModel?> GetByIdAsync(Guid id)
    {
        var sql = $"{ChamadaQuery.SELECT} WHERE id_chamada = @idChamada";

        using var conexao = new SqlConnection(Global.Conexao);
        var chamada = await conexao.QueryFirstOrDefaultAsync<ChamadaModel>(sql, new { idChamada = id });

        return chamada;
    }

    public static async Task<ChamadaModel?> GetChamadaAbertaAsync(int idPallet, long idAreaArmazenagem, int idAtividade)
    {
        var sql = $@"{ChamadaQuery.SELECT} 
                     WHERE id_palletorigem = @idPallet
                           AND fg_status < 4
                           AND id_areaarmazenagemorigem = @idAreaArmazenagem
                           AND id_atividade = @idAtividade";

        using var conexao = new SqlConnection(Global.Conexao);
        var chamada = await conexao.QueryFirstOrDefaultAsync<ChamadaModel>(sql, new { idPallet, idAreaArmazenagem, idAtividade });

        return chamada;
    }

    public static async Task<Guid> SetChamadaAsync(ChamadaInsertDTO chamada)
    {
        chamada.Codigo = Guid.NewGuid();
        chamada.Status = StatusChamada.Dependente;

        object chamadaInsert = new
        {
            id_chamada = chamada.Codigo,
            id_palletorigem = chamada.PalletOrigemId,
            id_palletdestino = chamada.PalletDestinoId,
            id_areaarmazenagemorigem = chamada.AreaArmazenagemOrigemId,
            id_areaarmazenagemdestino = chamada.AreaArmazenagemDestinoId,
            id_atividade = chamada.AtividadeId,
            fg_status = chamada.Status,
            priorizar = chamada.Priorizar,
        };

        using var conexao = new SqlConnection(Global.Conexao);
        await conexao.ExecuteAsync(ChamadaQuery.INSERT, chamadaInsert);

        return chamada.Codigo;
    }

    public static async Task<bool> SetStatusAsync(Guid idChamada, StatusChamada status)
    {
        using var conexao = new SqlConnection(Global.Conexao);
        var id = await conexao.ExecuteAsync(ChamadaQuery.UPDATE_STATUS, new { status = (int)status, idChamada });

        return id > 0;
    }

    public static async Task<bool> SetChamadaOrigem(Guid idChamada, Guid idChamadaOrigem)
    {
        using var conexao = new SqlConnection(Global.Conexao);
        var id = await conexao.ExecuteAsync(ChamadaQuery.UPDATE_CHAMADA_ORIGEM, new { idChamadaOrigem, idChamada });

        return id > 0;
    }

    public static async Task<bool> FinalizarChamadaAsync(Guid idChamada)
    {
        using var conexao = new SqlConnection(Global.Conexao);
        await conexao.ExecuteAsync(ChamadaQuery.UPDATE_FINALIZAR, new
        {
            status = StatusChamada.Finalizado,
            idChamada
        });

        return true;
    }

    public static async Task<bool> ReiniciarChamadaAsync(Guid idChamada)
    {
        var sql = $"{ChamadaQuery.UPDATE} AND fg_status < {StatusChamada.Rejeitado}";

        using var conexao = new SqlConnection(Global.Conexao);
        var chamada = await conexao.ExecuteAsync(sql, new
        {
            status = (int)StatusChamada.Aguardando,
            idEquipamento = DBNull.Value,
            dataRecebida = DBNull.Value,
            dataAtendida = DBNull.Value,
            dataFinalizada = DBNull.Value,
            idChamada
        });

        return chamada > 0;
    }

    public static async Task<bool> RejeitarChamadaAsync(Guid idChamada, int idAtividadeRejeicao)
    {
        using var conexao = new SqlConnection(Global.Conexao);
        await conexao.ExecuteAsync(ChamadaQuery.UPDATE_REJEITAR, new
        {
            statusRejeicao = (int)StatusChamada.Rejeitado,
            idAtividadeRejeicao,
            idChamada,
        });

        return true;
    }

    public static async Task<List<ChamadaModel>> GetByStatus(int fg_status)
    {
        var sql = $@"{ChamadaQuery.SELECT} WHERE fg_status < @fg_status and dt_chamada >= dateadd(day,-1,getdate())";

        using var conexao = new SqlConnection(Global.Conexao);

        var list = await conexao.QueryAsync<ChamadaModel>(sql, new { fg_status });

        return list.ToList();
    }
}
