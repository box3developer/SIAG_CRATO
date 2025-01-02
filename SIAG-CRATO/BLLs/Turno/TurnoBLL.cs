using Dapper;
using Microsoft.Data.SqlClient;
using SIAG_CRATO.DTOs.Turno;
using SIAG_CRATO.Models;

namespace SIAG_CRATO.BLLs.Turno;

public class TurnoBLL
{
    public static async Task<List<TurnoModel>> GetList()
    {
        var sql = $"{TurnoQuery.SELECT} ORDER BY dt_fim DESC";

        using var conexao = new SqlConnection(Global.Conexao);
        var turnos = await conexao.QueryAsync<TurnoModel>(sql);

        return turnos.ToList();
    }

    private static TurnoDTO ConvertToDTO(TurnoModel turno)
    {
        return new()
        {
            CodigoTurno = turno.CodTurno,
            DataInicio = turno.DtInicio,
            DataFim = turno.DtFim,
        };
    }
}
