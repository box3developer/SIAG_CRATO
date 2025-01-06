using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG.Domain.Armazenagem.Cadastro.Models

{
    [Table("regiaotrabalho")]
    public class RegiaoTrabalho
    {
        [Key]
        [Column("id_regiaotrabalho")]
        public int IdRegiaoTrabalho { get; set; }

        [ForeignKey("deposito")]
        [Column("id_deposito")]
        public int IdDeposito { get; set; }
        public Deposito? Deposito { get; set; }

        [Column("nm_regiaotrabalho")]
        public string NmRegiaoTrabalho { get; set; }
    }
}
