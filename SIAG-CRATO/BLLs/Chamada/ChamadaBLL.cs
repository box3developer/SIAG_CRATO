using Dapper;
using Microsoft.Data.SqlClient;
using SIAG_CRATO.Data;
using SIAG_CRATO.DTOs.Chamada;
using SIAG_CRATO.Models;

namespace SIAG_CRATO.BLLs.Chamada;

public class ChamadaBLL
{
    public static async Task<ChamadaDTO?> GetByIdAsync(Guid id)
    {
        var sql = $"{ChamadaQuery.SELECT} WHERE id_chamada = @idChamada";

        using var conexao = new SqlConnection(Global.Conexao);
        var chamada = await conexao.QueryFirstOrDefaultAsync<ChamadaModel>(sql, new { idChamada = id });

        if (chamada == null)
        {
            return null;
        }

        return ConvertToDTO(chamada);
    }

    public static async Task<ChamadaDTO?> GetChamadaAbertaAsync(int idPallet, long idAreaArmazenagem, int idAtividade)
    {
        var sql = $@"{ChamadaQuery.SELECT} 
                     WHERE id_palletorigem = @idPallet
                           AND fg_status < 4
                           AND id_areaarmazenagemorigem = @idAreaArmazenagem
                           AND id_atividade = @idAtividade";

        using var conexao = new SqlConnection(Global.Conexao);
        var chamada = await conexao.QueryFirstOrDefaultAsync<ChamadaModel>(sql, new { idPallet, idAreaArmazenagem, idAtividade });

        if (chamada == null)
        {
            return null;
        }

        return ConvertToDTO(chamada);
    }

    public static async Task<List<ChamadaDisponivelDTO>> GetChamadaDisponiveisAsync(int idEquipamentoModelo, int idSetorTrabalho)
    {
        var sql = $@"{ChamadaQuery.SELECT_DISPONIVEL} 
                     WHERE chamada.fg_status = @status
	                       AND chamada.id_operador IS NULL
	                       AND chamada.id_equipamento IS NULL
	                       AND chamada.dt_finalizada IS NULL
	                       AND atividade.id_equipamentomodelo = @idEquipamentoModelo
	                       AND atividade.id_setortrabalho = @idSetorTrabalho";

        using var conexao = new SqlConnection(Global.Conexao);
        var chamadas = await conexao.QueryAsync<ChamadaDisponivelDTO>(sql, new
        {
            status = StatusChamada.Andamento,
            idEquipamentoModelo,
            idSetorTrabalho
        });

        return chamadas.ToList();
    }

    public static async Task<ChamadaDTO?> GetChamadaAbertaByOperadorAsync(int idOperador, int idEquipamento)
    {
        var sql = $@"{ChamadaQuery.SELECT} 
                     WHERE (id_operador = @idOperador or id_equipamento = @idEquipamento)
                           AND fg_status > @status
                     ORDER BY fg_status desc, dt_chamada desc";

        using var conexao = new SqlConnection(Global.Conexao);
        var chamada = await conexao.QueryFirstOrDefaultAsync<ChamadaModel>(sql, new { idOperador, idEquipamento, StatusChamada.Andamento });

        if (chamada == null)
        {
            return null;
        }

        return ConvertToDTO(chamada);
    }

    public static async Task<Guid> SetChamadaAsync(ChamadaInsertDTO chamada)
    {
        chamada.IdChamada = Guid.NewGuid();
        chamada.FgStatus = StatusChamada.Dependente;

        using var conexao = new SqlConnection(Global.Conexao);
        await conexao.ExecuteAsync(ChamadaQuery.INSERT, new
        {
            idChamada = chamada.IdChamada,
            idPalletOrigem = chamada.IdPalletOrigem,
            idPalletDestino = chamada.IdPalletDestino,
            idAreaArmazenagemOrigem = chamada.IdAreaArmazenagemOrigem,
            idAreaArmazenagemDestino = chamada.IdAreaArmazenagemDestino,
            idAtividade = chamada.IdAtividade,
            statusChamada = chamada.FgStatus,
            priorizar = chamada.FgPriorizar,
        });

        return chamada.IdChamada;
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

    public static async Task<List<ChamadaDTO>> GetByStatus(int fg_status)
    {
        var sql = $@"{ChamadaQuery.SELECT} WHERE fg_status < @fg_status and dt_chamada >= dateadd(day,-1,getdate())";

        using var conexao = new SqlConnection(Global.Conexao);

        var lista = await conexao.QueryAsync<ChamadaModel>(sql, new { fg_status });

        return lista.Select(ConvertToDTO).ToList();
    }

    private static ChamadaDTO ConvertToDTO(ChamadaModel chamada)
    {
        return new()
        {
            IdChamada = chamada.IdChamada,
            IdPalletOrigem = chamada.IdPalletOrigem,
            IdPalletLeitura = chamada.IdPalletLeitura,
            IdPalletDestino = chamada.IdPalletDestino,
            IdAreaArmazenagemOrigem = chamada.IdAreaArmazenagemOrigem,
            IdAreaArmazenagemDestino = chamada.IdAreaArmazenagemDestino,
            IdAreaArmazenagemLeitura = chamada.IdAreaArmazenagemLeitura,
            IdOperador = chamada.IdOperador,
            IdEquipamento = chamada.IdEquipamento,
            IdAtividadeRejeicao = chamada.IdAtividadeRejeicao,
            IdAtividade = chamada.IdAtividade,
            FgStatus = chamada.FgStatus,
            DtChamada = chamada.DtChamada,
            DtRecebida = chamada.DtRecebida,
            DtAtendida = chamada.DtAtendida,
            DtFinalizada = chamada.DtFinalizada,
            DtRejeitada = chamada.DtRejeitada,
            DtSuspensa = chamada.DtSuspensa,
            IdChamadaSuspensa = chamada.IdChamadaSuspensa,
        };
    }
}
