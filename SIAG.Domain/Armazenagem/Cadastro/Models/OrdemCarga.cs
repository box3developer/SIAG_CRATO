using SIAG.Domain.Armazenagem.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SIAG.Domain.Armazenagem.Cadastro.Models
{
    [BasicEntity]
    [Table("ordemcarga")]
    public class OrdemCarga
    {
        [Key]
        [Column("id_ordemcarga")]
        public int IdOrdemcarga { get; set; }

        [Column("id_ordem")]
        public int IdOrdem { get; set; }

        [Column("id_pedido")]
        public int IdPedido { get; set; }

        [Column("id_programa")]
        public int IdPrograma { get; set; }

        [Column("id_pallet")]
        public int IdPallet { get; set; }
    }
}
