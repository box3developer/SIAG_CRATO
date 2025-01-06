using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG.Domain.Armazenagem.Cadastro.Models
{
    [Table("tipoendereco")]
    public class TipoEndereco
    {
        [Key]
        [Column("id_tipo_endereco")]
        public int IdTipoEndereco { get; set; }

        [Column("nm_tipo_endereco")]
        public string NmTipoEndereco { get; set; } = string.Empty;
    }
}
