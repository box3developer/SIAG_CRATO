using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SIAG.Domain.Armazenagem.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG.Domain.Armazenagem.Core.Models
{
    [BasicEntity]
    [Table("desempenho")]
    public class Desempenho
    {
        [Key]
        [Column("id_desempenho")]
        public long IdDesempenho { get; set; }

        [Column("dt_cadastro")]
        public DateTime DtCadastro { get; set; }

        [Column("fg_erroclassificacao")]
        public int? FgErroclassificacao { get; set; }

        [Column("fg_humoreficiencia")]
        public int? FgHumoreficiencia { get; set; }


        [Column("id_areaarmazenagem")]
        public long? IdAreaArmazenagem { get; set; }

        [ForeignKey(nameof(IdAreaArmazenagem))]
        public AreaArmazenagem? AreaArmazenagem { get; set; }


        [Column("id_equipamento")]
        public int? IdEquipamento { get; set; }

        [ForeignKey(nameof(IdEquipamento))]
        public Equipamento? Equipamento { get; set; }


        [Column("id_equipamentomodelo")]
        public int? IdEquipamentoModelo { get; set; }

        [ForeignKey(nameof(IdEquipamentoModelo))]
        public EquipamentoModelo? EquipamentoModelo { get; set; }


        [Column("id_operador")]
        public long? IdOperador { get; set; }

        [ForeignKey(nameof(IdOperador))]
        public Operador? Operador { get; set; }


        [Column("id_referencia")]
        public string? IdReferencia { get; set; } = string.Empty;


        [Column("id_setortrabalho")]
        public int? IdSetorTrabalho { get; set; }

        [ForeignKey(nameof(IdSetorTrabalho))]
        public SetorTrabalho? SetorTrabalho { get; set; }


        [Column("nr_tempoestimado")]
        public int? NrTempoestimado { get; set; }

        [Column("nr_temporealizado")]
        public int? NrTemporealizado { get; set; }
    }
}
