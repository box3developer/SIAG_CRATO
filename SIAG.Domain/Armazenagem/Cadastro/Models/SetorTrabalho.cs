using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG.Domain.Armazenagem.Cadastro.Models
{
    [Table("setortrabalho")]
    public class SetorTrabalho
    {
        [Key]
        [Column("id_setor_trabalho")]
        public int IdSetorTrabalho { get; set; }

        [ForeignKey("Deposito")]
        [Column("id_deposito")]
        public int IdDeposito { get; set; }
        public Deposito? Deposito { get; set; }

        [Column("nm_setor_trabalho")]
        public string? NmSetorTrabalho { get; set; }
    }
}
