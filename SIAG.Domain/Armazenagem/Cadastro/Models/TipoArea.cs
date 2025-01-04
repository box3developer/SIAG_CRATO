using System.ComponentModel.DataAnnotations;

namespace SIAG.Domain.Armazenagem.Cadastro.Models
{
    public class TipoArea
    {
        [Key]
        public int TipoAreaId { get; set; }

        public string NmTipoArea { get; set; }
    }
}
