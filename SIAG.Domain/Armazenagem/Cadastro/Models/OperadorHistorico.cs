using SIAG.Domain.Armazenagem.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG.Domain.Armazenagem.Cadastro.Models
{
    [KeylessEntity]
    [Table("operadorhistorico")]
    public class OperadorHistorico
    {
        [Column("id_operador")]
        public long? IdOperador { get; set; }


        [Column("cd_evento")]
        public int? CdEvento { get; set; }

        [Column("dt_evento")]
        public DateTime? DtEvento { get; set; }


        [Column("id_endereco")]
        public int? IdEndereco { get; set; }

        [ForeignKey(nameof(IdEndereco))]
        public Endereco? Endereco { get; set; }


        [Column("id_equipamento")]
        public int? IdEquipamento { get; set; }

        [ForeignKey(nameof(IdEquipamento))]
        public Equipamento? Equipamento { get; set; }
    }
}
