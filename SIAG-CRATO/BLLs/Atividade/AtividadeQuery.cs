namespace SIAG_CRATO.BLLs.Atividade;

public class AtividadeQuery
{
    public const string SELECT = "SELECT id_atividade, nm_atividade, id_equipamentomodelo, fg_permite_rejeitar, id_atividadeanterior, id_setortrabalho, fg_tipoatribuicaoautomatica, id_atividaderotinavalidacao, fg_evitaconflitoendereco FROM atividade WITH(NOLOCK)";
}
