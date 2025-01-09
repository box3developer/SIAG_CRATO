using SIAG.Domain.Armazenagem.Cadastro.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG.Domain.Armazenagem.Cadastro.Models
{
    [BasicEntity]
    [Table("tipoendereco")]
    public class TipoEndereco
    {
        [Key]
        [Column("id_tipoendereco")]
        public int IdTipoEndereco { get; set; }

        [Column("nm_tipoendereco")]
        public string NmTipoEndereco { get; set; } = string.Empty;
    }
}
