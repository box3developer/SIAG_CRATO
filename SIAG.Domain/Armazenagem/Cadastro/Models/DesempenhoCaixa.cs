using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG.Domain.Armazenagem.Cadastro.Models

{
    [Table("desempenhocaixa")]
    public class Desempenhocaixa
    {
        [Key]
        [Column("id_desempenhocaixa")]
        public string IdDesempenhocaixa { get; set; } = string.Empty;

        [Column("id_caixa")]
        public string IdCaixa { get; set; } = string.Empty;

        [Column("dt_leituracaixa")]
        public DateTime DtLeituracaixa { get; set; }

        [Column("fg_erroclassificacao")]
        public int FgErroclassificacao { get; set; }

        [Column("id_areaarmazenagem")]
        public long IdAreaarmazenagem { get; set; }

        [Column("id_equipamento")]
        public int IdEquipamento { get; set; }

        [Column("id_operador")]
        public long IdOperador { get; set; }

        [Column("nr_tempoestimado")]
        public int NrTempoestimado { get; set; }

    }

}
