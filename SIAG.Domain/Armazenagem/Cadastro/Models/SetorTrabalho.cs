using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SIAG.Domain.Armazenagem.Cadastro.Models
{
    [Table("setortrabalho")]
    public class SetorTrabalho
    {
        [Key]
        [Column("id_setortrabalho")]
        public int SetorTrabalhoId { get; set; }

        [ForeignKey("Deposito")]
        [Column("id_deposito")]
        public int DepositoId { get; set; }
        public Deposito Deposito { get; set; }

        [Column("nm_setortrabalho")]
        public string NmSetorTrabalho { get; set; }

        [JsonIgnore]
        public List<Endereco> Enderecos { get; set; }
    }
}
