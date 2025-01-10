using SIAG.Domain.Armazenagem.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG.Domain.Armazenagem.Core.Models
{
    [BasicEntity]
    [Table("tipoarea")]
    public class TipoArea
    {
        [Key]
        [Column("id_tipoarea")]
        public int IdTipoArea { get; set; }

        [Column("nm_tipoarea")]
        public string NmTipoArea { get; set; } = string.Empty;
    }
}
