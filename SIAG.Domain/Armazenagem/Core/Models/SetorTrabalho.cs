using SIAG.Domain.Armazenagem.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG.Domain.Armazenagem.Core.Models
{
    [BasicEntity]
    [Table("setortrabalho")]
    public class SetorTrabalho
    {
        [Key]
        [Column("id_setortrabalho")]
        public int IdSetorTrabalho { get; set; }

        [Column("id_deposito")]
        public int IdDeposito { get; set; }

        [ForeignKey(nameof(IdDeposito))]
        public Deposito? Deposito { get; set; }

        [Column("nm_setortrabalho")]
        public string? NmSetorTrabalho { get; set; }
    }
}
