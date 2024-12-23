using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG_CRATO.Models;

public class AtividadeTarefaModel
{
    [Column("id_tarefa")]
    public int Codigo { get; set; }

    [Column("nm_tarefa")]
    public string Descricao { get; set; } = string.Empty;

    [Column("nm_mensagem")]
    public string Mensagem { get; set; } = string.Empty;

    [Column("id_atividade")]
    public int AtividadeId { get; set; }

    [Column("cd_sequencia")]
    public int Sequencia { get; set; }

    [Column("fg_recurso")]
    public int? Recursos { get; set; }

    [Column("id_atividaderotina")]
    public int AtividadeRotinaId { get; set; }

    [Column("qt_potencianormal")]
    public int PotenciaNormal { get; set; }

    [Column("qt_potenciaaumentada")]
    public int PotenciaAumentada { get; set; }
}
