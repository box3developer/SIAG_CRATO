namespace SIAG_CRATO.BLLs.ChamadaPendencia;

public class ChamadaPendenciaQuery
{
    public const string UPDATE_CHAMADA_PAI = @"UPDATE chamadadependencia SET id_chamada = @idChamadaPai WHERE id_chamada = @idChamada";
    public const string UPDATE_CHAMADA_ORIGEM = @"UPDATE chamadadependencia SET id_chamadapai = @idChamadaOrigem WHERE id_chamadapai = @idChamada";
}
