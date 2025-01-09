using SIAG.Domain.Armazenagem.Cadastro.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG.Domain.Armazenagem.Cadastro.Models
{
    [CustomKeyEntity]
    [Table("niveisagrupadores")]
    public class Niveisagrupadores
    {
        [Key]
        [Column("id_niveisagrupadores")]
        public long IdNiveisagrupadores { get; set; }

        [Column("inicio")]
        public long Inicio { get; set; }

        [Column("nivel")]
        public int Nivel { get; set; }

        [Column("termino")]
        public long? Termino { get; set; }

    }
}
