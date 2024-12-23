namespace SIAG_CRATO.BLLs.AtividadeTarefa;

public class AtividadeTarefaQuery
{
    public const string SELECT = "SELECT id_tarefa, nm_tarefa, nm_mensagem, id_atividade, cd_sequencia, fg_recurso, id_atividaderotina, qt_potencianormal, qt_potenciaaumentada FROM atividadetarefa WITH(NOLOCK)";
}
