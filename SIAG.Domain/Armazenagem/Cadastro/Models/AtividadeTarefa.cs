using SIAG.Domain.Armazenagem.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG.Domain.Armazenagem.Cadastro.Models;

[CustomKeyEntity]
[Table("atividade_tarefa")]
public class AtividadeTarefa
{
    [Key]
    [Column("id_tarefa")]
    public int IdTarefa { get; set; }

    [Column("nm_tarefa")]
    public string NmTarefa { get; set; } = string.Empty;

    [Column("nm_mensagem")]
    public string NmMensagem { get; set; } = string.Empty;

    [Column("id_atividade")]
    public int IdAtividade { get; set; }

    [Column("cd_sequencia")]
    public int CdSequencia { get; set; }

    [Column("fg_recurso")]
    public int? FgRecurso { get; set; }

    [Column("id_atividaderotina")]
    public int IdAtividadeRotina { get; set; }

    [ForeignKey(nameof(IdAtividadeRotina))]
    public AtividadeRotina? AtividadeRotina { get; set; }

    [Column("qt_potencianormal")]
    public int QtPotenciaNormal { get; set; }

    [Column("qt_potenciaaumentada")]
    public int QtPotenciaAumentada { get; set; }
}
