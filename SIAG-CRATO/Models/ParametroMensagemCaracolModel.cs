using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG_CRATO.Models;

public class ParametroMensagemCaracolModel
{
    [Column("id_parametromensagemcaracol")]
    public int IdParametroMensagemCaracol { get; set; }

    [Column("descricao")]
    public string? Descricao { get; set; }

    [Column("mensagem")]
    public string? Mensagem { get; set; }

    [Column("cor")]
    public string? Cor { get; set; }
}
