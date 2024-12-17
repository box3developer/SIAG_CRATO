namespace SIAG_CRATO.BLLs.Turno;

public class TurnoQuery
{
    public const string SELECT = "SELECT cd_turno, dt_inicio, dt_fim FROM turno WITH(NOLOCK)";
}
