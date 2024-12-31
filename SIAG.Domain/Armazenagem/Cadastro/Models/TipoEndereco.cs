using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SIAG.Domain.Armazenagem.Cadastro.Models
{
    [Table("tipoendereco")]
    public class TipoEndereco
    {
        [Key]
        [Column("id_tipoendereco")]
        public int TipoEnderecoId { get; set; }

        [Column("nm_tipoendereco")]
        public string NmTipoEndereco { get; set; }

        [JsonIgnore]
        public List<Endereco> EnderecosModel { get; set; }
    }
}
