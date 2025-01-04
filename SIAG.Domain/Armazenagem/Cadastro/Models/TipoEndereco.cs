using System.ComponentModel.DataAnnotations;

namespace SIAG.Domain.Armazenagem.Cadastro.Models
{
    public class TipoEndereco
    {
        [Key]
        public int TipoEnderecoId { get; set; }

        public string NmTipoEndereco { get; set; }
    }
}
