using SIAG.Domain.Armazenagem.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG.Domain.Chamada.Cadastro.Models;

[CustomKeyEntity]
[Table("atividadeprioridade")]
public class AtividadePrioridade
{
    [Key]
    [Column("id_atividadeprioridade")]
    public int IdAtividadePrioridade { get; set; }

    [Column("fg_tipo")]
    public int FgTipo { get; set; }

    [Column("nm_procedure")]
    public string NmProcedure { get; set; } = string.Empty;

    [Column("qt_pontuacao")]
    public int QtPontuacao { get; set; }
}
