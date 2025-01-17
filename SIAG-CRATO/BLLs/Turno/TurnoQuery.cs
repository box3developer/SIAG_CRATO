namespace SIAG_CRATO.BLLs.Turno;

public class TurnoQuery
{
    public const string SELECT = "SELECT cd_turno, dt_inicio, dt_fim FROM turno WITH(NOLOCK)";

    public const string SELECT_PERFORMANCE = @"SELECT cd_turno, 
													  DATEADD(DAY, DATEDIFF(DAY, 0, GETDATE()), CAST(CAST(dt_inicio AS TIME) AS VARCHAR(10))) AS dt_inicio,
													  CASE
													      WHEN CAST(dt_inicio AS DATE) = cast(dt_fim AS DATE)
														     THEN DATEADD(DAY, DATEDIFF(DAY, 0, getdate()), CAST(CAST(dt_fim AS TIME) AS VARCHAR(10)))
														  ELSE
														     DATEADD(DAY, DATEDIFF(DAY, -1, GETDATE()), CAST(CAST(dt_fim AS TIME) AS VARCHAR(10)))
												      END AS dt_fim
												FROM turno WITH(NOLOCK)
												ORDER BY cd_turno";
}
