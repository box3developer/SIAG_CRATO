using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SIAG.Domain.Armazenagem.Cadastro.Models
{
    public class Deposito
    {
        public int DepositoId { get; set; }

        public string NmDeposito { get; set; }
    }
}
