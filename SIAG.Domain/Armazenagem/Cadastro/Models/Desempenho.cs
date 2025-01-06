using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG.Domain.Armazenagem.Cadastro.Models
{
    [Table("desempenho")]
    public class Desempenho
    {
        [Key]
        [Column("id_desempenho")]
        public long IdDesempenho { get; set; }

        [Column("dt_cadastro")]
        public DateTime DtCadastro { get; set; }

        [Column("fg_erroclassificacao")]
        public int FgErroclassificacao { get; set; }

        [Column("fg_humoreficiencia")]
        public int FgHumoreficiencia { get; set; }

        [Column("id_areaarmazenagem")]
        public long IdAreaarmazenagem { get; set; }

        [Column("id_equipamento")]
        public int IdEquipamento { get; set; }

        [Column("id_equipamentomodelo")]
        public int IdEquipamentomodelo { get; set; }

        [Column("id_operador")]
        public long IdOperador { get; set; }

        [Column("id_referencia")]
        public string IdReferencia { get; set; } = string.Empty;

        [Column("id_setortrabalho")]
        public int IdSetortrabalho { get; set; }

        [Column("nr_tempoestimado")]
        public int NrTempoestimado { get; set; }

        [Column("nr_temporealizado")]
        public int NrTemporealizado { get; set; }

    }

}
