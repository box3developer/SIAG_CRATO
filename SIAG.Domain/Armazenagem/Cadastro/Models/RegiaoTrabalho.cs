using SIAG.Domain.Armazenagem.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG.Domain.Armazenagem.Cadastro.Models

{
    [BasicEntity]
    [Table("regiaotrabalho")]
    public class RegiaoTrabalho
    {
        [Key]
        [Column("id_regiaotrabalho")]
        public int IdRegiaoTrabalho { get; set; }

        [Column("id_deposito")]
        public int IdDeposito { get; set; }

        [ForeignKey(nameof(IdDeposito))]
        public Deposito? Deposito { get; set; }

        [Column("nm_regiaotrabalho")]
        public string NmRegiaoTrabalho { get; set; } = string.Empty;
    }
}
