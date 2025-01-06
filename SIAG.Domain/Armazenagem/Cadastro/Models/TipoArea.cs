using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG.Domain.Armazenagem.Cadastro.Models
{
    [Table("tipoarea")]
    public class TipoArea
    {
        [Key]
        [Column("id_tipo_area")]
        public int IdTipoArea { get; set; }

        [Column("nm_tipo_area")]
        public string NmTipoArea { get; set; } = string.Empty;
    }
}
