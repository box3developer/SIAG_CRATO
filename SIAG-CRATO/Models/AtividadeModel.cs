using System.ComponentModel.DataAnnotations.Schema;
using SIAG_CRATO.Data;

namespace SIAG_CRATO.Models;
public class AtividadeModel
{
    [Column("id_atividade")]
    public int Codigo { get; set; }

    [Column("nm_atividade")]
    public string Descricao { get; set; } = string.Empty;

    [Column("id_equipamentomodelo")]
    public int EquipamentoModeloId { get; set; }

    [Column("id_atividaderotinavalidacao")]
    public int AtividadeRotinaValidacaoId { get; set; }

    [Column("id_atividadeanterior")]
    public int AtividadeAnteriorId { get; set; }

    [Column("id_setortrabalho")]
    public int SetorTrabalhoId { get; set; }

    [Column("fg_permite_rejeitar")]
    public RejeicaoTarefa PermiteRejeitar { get; set; }

    [Column("fg_tipoatribuicaoautomatica")]
    public TipoAtribuicaoAutomatica TipoAtribuicaoAutomatica { get; set; }

    [Column("fg_evitaconflitoendereco")]
    public ConflitoDeEnderecos EvitarConflitoEndereco { get; set; }
}
