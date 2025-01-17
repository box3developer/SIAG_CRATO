using Dapper;
using Microsoft.Data.SqlClient;
using SIAG_CRATO.DTOs.Turno;
using SIAG_CRATO.Models;

namespace SIAG_CRATO.BLLs.Turno;

public class TurnoBLL
{
    public static async Task<List<TurnoDTO>> GetList()
    {
        var sql = $"{TurnoQuery.SELECT} ORDER BY dt_fim DESC";

        using var conexao = new SqlConnection(Global.Conexao);
        var turnos = await conexao.QueryAsync<TurnoModel>(sql);

        return turnos.Select(ConvertToDTO).ToList();
    }

    public static async Task<List<TurnoDTO>> GetListPerformance()
    {
        using var conexao = new SqlConnection(Global.Conexao);
        var turnos = await conexao.QueryAsync<TurnoModel>(TurnoQuery.SELECT_PERFORMANCE);

        return turnos.Select(ConvertToDTO).ToList();
    }
    private static TurnoDTO ConvertToDTO(TurnoModel turno)
    {
        return new()
        {
            CdTurno = turno.CdTurno,
            DtInicio = turno.DtInicio,
            DtFim = turno.DtFim,
        };
    }
}
