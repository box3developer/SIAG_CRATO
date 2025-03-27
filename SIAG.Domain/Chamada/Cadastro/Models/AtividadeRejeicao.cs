using SIAG.Domain.Armazenagem.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG.Domain.Chamada.Cadastro.Models;

[CustomKeyEntity]
[Table("atividaderejeicao")]
public class AtividadeRejeicao
{
    [Key]
    [Column("id_atividaderejeicao")]
    public int IdAtividadeRejeicao { get; set; }

    [Column("nm_atividaderejeicao")]
    public string NmAtividadeRejeicao { get; set; } = string.Empty;

    [Column("nm_email_alerta")]
    public string NmEmailAlerta { get; set; } = string.Empty;
}
