using SIAG.Domain.Armazenagem.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG.Domain.Chamada.Cadastro.Models;

[BasicEntity]
[Table("atividaderotina")]
public class AtividadeRotina
{
    [Key]
    [Column("id_atividaderotina")]
    public int IdAtividadeRotina { get; set; }

    [Column("nm_atividaderotina")]
    public string NmAtividadeRotina { get; set; } = string.Empty;

    [Column("nm_procedure")]
    public string NmProcedure { get; set; } = string.Empty;

    [Column("fg_tipo")]
    public int FgTipo { get; set; }
}
