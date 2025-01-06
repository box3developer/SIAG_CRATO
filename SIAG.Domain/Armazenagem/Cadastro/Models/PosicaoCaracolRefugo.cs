using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG.Domain.Armazenagem.Cadastro.Models
{
    [Table("posicaocaracolrefugo")]
    public class Posicaocaracolrefugo
    {
        [Key]
        [Column("id_posicaocaracolrefugo")]
        public int IdPosicaocaracolrefugo { get; set; }

        [Column("descricao")]
        public string Descricao { get; set; } = string.Empty;

        [Column("fabrica")]
        public string? Fabrica { get; set; } = string.Empty;

        [Column("posicao")]
        public int Posicao { get; set; }

        [Column("tipo")]
        public string Tipo { get; set; } = string.Empty;
    }
}
