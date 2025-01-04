using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG.Domain.Armazenagem.Cadastro.Models
{
    public class SetorTrabalho
    {
        [Key]
        public int SetorTrabalhoId { get; set; }

        [ForeignKey("DepositoModel")] 
        public int DepositoId { get; set; }
        public Deposito Deposito { get; set; }

        public string NmSetorTrabalho { get; set; }
    }
}
