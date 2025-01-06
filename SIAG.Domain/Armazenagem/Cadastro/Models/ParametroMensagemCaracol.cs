using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG.Domain.Armazenagem.Cadastro.Models
{
    [Table("parametromensagemcaracol")]
    public class Parametromensagemcaracol
    {
        [Key]
        [Column("id_parametromensagemcaracol")]
        public int IdParametromensagemcaracol { get; set; }

        [Column("cor")]
        public string Cor { get; set; } = string.Empty;

        [Column("descricao")]
        public string Descricao { get; set; } = string.Empty;

        [Column("mensagem")]
        public string Mensagem { get; set; } = string.Empty;

    }
}
