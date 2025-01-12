using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SIAG.Application.Armazenagem.Core.DTOs
{
    public class DepositoDTO
    {
        public int IdDeposito { get; set; }

        public string NmDeposito { get; set; }
    }
}
