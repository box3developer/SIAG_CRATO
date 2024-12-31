using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SIAG.Domain.Armazenagem.Cadastro.Models
{
    [Table("tipoarea")]
    public class TipoArea
    {
        [Key]
        [Column("id_tipoarea")]
        public int TipoAreaId { get; set; }

        [Column("nm_tipoarea")]
        public string NmTipoArea { get; set; }

        [JsonIgnore]
        public List<AreaArmazenagem> AreasArmazenagemModel { get; set; }
    }
}
