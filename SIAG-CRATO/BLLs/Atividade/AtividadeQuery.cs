namespace SIAG_CRATO.BLLs.Atividade;

public class AtividadeQuery
{
    public const string SELECT = @"SELECT id_atividade, nm_atividade, id_equipamentomodelo, fg_permite_rejeitar, id_atividadeanterior, id_setortrabalho, fg_tipoatribuicaoautomatica, id_atividaderotinavalidacao, fg_evitaconflitoendereco, fg_tipoatividade, duracao FROM atividade WITH(NOLOCK)";

    public const string SELECT_PRIORIDADE = @"SELECT ap.id_atividadeprioridade, cp.fg_tipo, cp.nm_procedure, ap.qt_pontuacao
											  FROM atividade a WITH(NOLOCK)
											  INNER JOIN atividadeprioridade ap WITH(NOLOCK) ON (ap.id_atividade = a.id_atividade)
											  INNER JOIN convocacaoprioridade cp WITH(NOLOCK) ON (cp.id_convocacaoprioridade = ap.id_convocacaoprioridade)
											  WHERE a.id_atividade = @idAtividade";
}
