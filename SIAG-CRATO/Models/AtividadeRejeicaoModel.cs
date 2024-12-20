using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG_CRATO.Models;

public class AtividadeRejeicaoModel
{
    [Column("id_atividaderejeicao")]
    public int Codigo { get; set; }

    [Column("nm_atividaderejeicao")]
    public string Descricao { get; set; } = string.Empty;

    [Column("nm_email_alerta")]
    public string Email { get; set; } = string.Empty;
}
