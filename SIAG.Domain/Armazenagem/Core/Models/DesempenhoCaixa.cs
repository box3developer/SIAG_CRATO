using SIAG.Domain.Armazenagem.Attributes;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG.Domain.Armazenagem.Core.Models

{
    [KeylessEntity]
    [Table("desempenhocaixa")]
    public class Desempenhocaixa
    {
        [Column("id_caixa")]
        public string? IdCaixa { get; set; } = string.Empty;

        [Column("dt_leituracaixa")]
        public DateTime? DtLeituracaixa { get; set; }

        [Column("fg_erroclassificacao")]
        public int? FgErroclassificacao { get; set; }


        [Column("id_areaarmazenagem")]
        public long? IdAreaArmazenagem { get; set; }

        [ForeignKey(nameof(IdAreaArmazenagem))]
        public AreaArmazenagem? AreaArmazenagem { get; set; }


        [Column("id_equipamento")]
        public int? IdEquipamento { get; set; }

        [ForeignKey(nameof(IdEquipamento))]
        public Equipamento? Equipamento { get; set; }


        [Column("id_operador")]
        public long? IdOperador { get; set; }

        [ForeignKey(nameof(IdOperador))]
        public Operador? Operador { get; set; }


        [Column("nr_tempoestimado")]
        public int? NrTempoestimado { get; set; }
    }
}
