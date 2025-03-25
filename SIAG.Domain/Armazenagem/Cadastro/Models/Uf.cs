using SIAG.Domain.Armazenagem.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG.Domain.Armazenagem.Cadastro.Models
{
    [KeylessEntity]
    [Table("uf")]
    public class Uf
    {
        [Column("nm_uf")]
        public string NmUf { get; set; } = string.Empty;

        [Column("nm_nomeuf")]
        public string NmNomeuf { get; set; } = string.Empty;
    }
}
