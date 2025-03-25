using SIAG.Domain.Armazenagem.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG.Domain.Armazenagem.Cadastro.Models;

[CustomKeyEntity]
[Table("atividaderejeicao")]
public class AtividadeRejeicaoM
{
    [Key]
    [Column("id_atividaderejeicao")]
    public int IdAtividadeRejeicao { get; set; }

    [Column("nm_atividade_rejeicao")]
    public string NmAtividadeRejeicao { get; set; } = string.Empty;

    [Column("nm_email_alerta")]
    public string NmEmailAlerta { get; set; } = string.Empty;
}
