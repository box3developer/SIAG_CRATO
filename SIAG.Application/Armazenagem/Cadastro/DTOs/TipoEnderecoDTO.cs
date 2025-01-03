using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SIAG.Domain.Armazenagem.Cadastro.Models
{
    public class TipoEnderecoDTO
    {
        public int TipoEnderecoId { get; set; }

        public string NmTipoEndereco { get; set; }
    }
}
