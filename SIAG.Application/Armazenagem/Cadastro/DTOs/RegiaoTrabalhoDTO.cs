using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SIAG.Application.Armazenagem.Cadastro.DTOs
{
    public class RegiaoTrabalhoDTO
    {
        public int RegiaoTrabalhoId { get; set; }

        public int DepositoId { get; set; }

        public string NmRegiaoTrabalho { get; set; }
    }
}
