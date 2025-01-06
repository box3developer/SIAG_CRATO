using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG.Domain.Armazenagem.Cadastro.Models
{
    [Table("deposito")]
    public class Deposito
    {
        [Key]
        [Column("id_deposito")]
        public int IdDeposito { get; set; }

        [Column("nm_deposito")]
        public string NmDeposito { get; set; } = string.Empty;
    }
}
