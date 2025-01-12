using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SIAG.Application.Armazenagem.Core.DTOs
{
    public class RegiaoTrabalhoDTO
    {
        public int IdRegiaoTrabalho { get; set; }

        public int IdDeposito { get; set; }

        public string NmRegiaoTrabalho { get; set; }
    }
}
