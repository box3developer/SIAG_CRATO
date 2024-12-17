namespace SIAG_CRATO.BLLs.ChamadaTarefa;

public class ChamadaTarefaQuery
{
    public const string SELECT = "SELECT id_tarefa, id_chamada FROM chamadatarefa WITH(NOLOCK)";
    public const string INSERT = "INSERT INTO chamadatarefa (id_chamada, id_tarefa) VALUES (@idChamada, @idTarefa)";
}
