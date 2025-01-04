using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG.Domain.Armazenagem.Cadastro.Models
{
    public class RegiaoTrabalho
    {
        [Key]
        public int RegiaoTrabalhoId { get; set; }

        [ForeignKey("DepositoModel")]
        public int DepositoId { get; set; }
        public Deposito Deposito { get; set; }

        public string NmRegiaoTrabalho { get; set; }
    }
}
