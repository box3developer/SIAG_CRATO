using System.ComponentModel.DataAnnotations;

namespace SIAG.Domain.Armazenagem.Cadastro.Models
{
    public class Deposito
    {
        [Key]
        public int DepositoId { get; set; }

        public string NmDeposito { get; set; }
    }
}
