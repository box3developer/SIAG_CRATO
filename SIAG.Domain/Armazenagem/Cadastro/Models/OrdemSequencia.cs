using SIAG.Domain.Armazenagem.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SIAG.Domain.Armazenagem.Cadastro.Models
{
    [BasicEntity]
    [Table("ordemsequencia")]
    public class OrdemSequencia
    {
        [Key]
        [Column("id_ordemsequencia")]
        public int IdOrdemsequencia { get; set; }

        [Column("id_ordem")]
        public int IdOrdem { get; set; }

        [Column("id_transportadoratipocarga")]
        public int IdTransportadoratipocarga { get; set; }

        [Column("nr_sequencia")]
        public int NrSequencia { get; set; }
    }
}
