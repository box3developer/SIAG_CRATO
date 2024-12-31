using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SIAG.Domain.Armazenagem.Cadastro.Models
{
    [Table("regiaotrabalho")]
    public class RegiaoTrabalho
    {
        [Key]
        [Column("id_regiaotrabalho")]
        public int RegiaoTrabalhoId { get; set; }

        [ForeignKey("Deposito")]
        [Column("id_deposito")]
        public int DepositoId { get; set; }

        [Column("nm_regiaotrabalho")]
        public string NmRegiaoTrabalho { get; set; }

        [JsonIgnore]
        public List<Endereco> Enderecos { get; set; }
    }
}
