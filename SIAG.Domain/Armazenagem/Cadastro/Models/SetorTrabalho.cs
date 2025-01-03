using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SIAG.Domain.Armazenagem.Cadastro.Models
{
    public class SetorTrabalho
    {
        public int SetorTrabalhoId { get; set; }

        public int DepositoId { get; set; }
        public Deposito Deposito { get; set; }

        public string NmSetorTrabalho { get; set; }
    }
}
