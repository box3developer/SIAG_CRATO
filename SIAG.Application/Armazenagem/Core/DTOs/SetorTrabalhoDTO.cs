using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SIAG.Application.Armazenagem.Core.DTOs
{
    public class SetorTrabalhoDTO
    {
        public int IdSetorTrabalho { get; set; }

        public int IdDeposito { get; set; }
        public DepositoDTO Deposito { get; set; }

        public string NmSetorTrabalho { get; set; }
    }
}
