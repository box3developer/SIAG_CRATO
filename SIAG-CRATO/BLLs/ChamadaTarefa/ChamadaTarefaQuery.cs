namespace SIAG_CRATO.BLLs.ChamadaTarefa;

public class ChamadaTarefaQuery
{
    public const string SELECT = "SELECT id_tarefa, id_chamada, dt_inicio, dt_fim FROM chamadatarefa WITH(NOLOCK)";
    public const string INSERT = "INSERT INTO chamadatarefa (id_chamada, id_tarefa) VALUES (@idChamada, @idTarefa)";
    public const string UPDATE = "UPDATE chamadatarefa SET dt_inicio = @dataInicio, dt_fim = @dataFim";
}
