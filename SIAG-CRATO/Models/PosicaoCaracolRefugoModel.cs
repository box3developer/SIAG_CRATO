using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG_CRATO.Models;

public class PosicaoCaracolRefugoModel
{
    [Column("id_posicaocaracolrefugo")]
    public int IdPosicaoCaracolRefugo { get; set; }

    [Column("descricao")]
    public string? Descricao { get; set; }

    [Column("posicao")]
    public int Posicao { get; set; }

    [Column("tipo")]
    public string? Tipo { get; set; }

    [Column("fabrica")]
    public string? Fabrica { get; set; }
}
