using SIAG.Domain.Armazenagem.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Intrinsics.Arm;

namespace SIAG.Domain.Armazenagem.Cadastro.Models
{
    [BasicEntity]
    [Table("caixaleitura")]
    public class Caixaleitura
    {
        [Key]
        [Column("id_caixaleitura")]
        public long IdCaixaleitura { get; set; }

        [Column("dt_leitura")]
        public DateTime? DtLeitura { get; set; }

        [Column("fg_cancelado")]
        public bool? FgCancelado { get; set; }

        [Column("fg_status")]
        public int? FgStatus { get; set; }

        [Column("fg_tipo")]
        public int? FgTipo { get; set; }


        [Column("id_areaarmazenagem")]
        public long? IdAreaarmazenagem { get; set; }

        [ForeignKey(nameof(IdAreaarmazenagem))]
        public AreaArmazenagem? Areaarmazenagem { get; set; }


        [Column("id_caixa")]
        public string? IdCaixa { get; set; }

        [ForeignKey(nameof(IdCaixa))]
        public Caixa? Caixa { get; set; }


        [Column("id_endereco")]
        public int? IdEndereco { get; set; }

        [ForeignKey(nameof(IdEndereco))]
        public Endereco? Endereco { get; set; }


        [Column("id_equipamento")]
        public int? IdEquipamento { get; set; }

        [ForeignKey(nameof(IdEquipamento))]
        public Equipamento? Equipamento { get; set; }


        [Column("id_operador")]
        public long? IdOperador { get; set; }

        [ForeignKey(nameof(IdOperador))]
        public Operador? Operador { get; set; }


        [Column("id_ordem")]
        public long? IdOrdem { get; set; }

        [Column("id_pallet")]
        public int? IdPallet { get; set; }

        [ForeignKey(nameof(IdPallet))]
        public Pallet? Pallet { get; set; }
    }
}
