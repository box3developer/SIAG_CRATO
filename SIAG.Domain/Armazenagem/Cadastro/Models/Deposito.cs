using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SIAG.Domain.Armazenagem.Cadastro.Models
{
    [Table("deposito")]
    public class Deposito
    {
        [Key]
        [Column("id_deposito")]
        public int DepositoId { get; set; }

        [Column("nm_deposito")]
        public string NmDeposito { get; set; }

        // foreign keys 
        [JsonIgnore]
        public List<SetorTrabalho> SetorTrabalhos { get; set; }
    }
}
