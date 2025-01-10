using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SIAG.Application.Armazenagem.Core.DTOs
{
    public class TipoAreaDTO
    {
        public int TipoAreaId { get; set; }

        public string NmTipoArea { get; set; }
   }
}
