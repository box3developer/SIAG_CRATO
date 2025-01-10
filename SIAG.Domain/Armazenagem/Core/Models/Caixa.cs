using SIAG.Domain.Armazenagem.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG.Domain.Armazenagem.Core.Models
{
    [CustomKeyEntity]
    [Table("caixa")]
    public class Caixa
    {
        [Key]
        [Column("id_caixa")]
        public string IdCaixa { get; set; } = string.Empty;


        [Column("id_agrupador")]
        public Guid? IdAgrupador { get; set; }

        [ForeignKey(nameof(IdAgrupador))]
        public AgrupadorAtivo? Agrupador { get; set; }


        [Column("id_pallet")]
        public int? IdPallet { get; set; }

        [ForeignKey(nameof(IdPallet))]
        public Pallet? Pallet { get; set; }


        [Column("id_programa")]
        public int? IdPrograma { get; set; }

        [ForeignKey(nameof(IdPrograma))]
        public Programa? Programa { get; set; }


        [Column("id_pedido")]
        public int? IdPedido { get; set; }

        [ForeignKey(nameof(IdPedido))]
        public Pedido? Pedido { get; set; }


        [Column("cd_produto")]
        public string? CdProduto { get; set; } = string.Empty;

        [Column("cd_cor")]
        public string? CdCor { get; set; } = string.Empty;

        [Column("cd_gradetamanho")]
        public string? CdGradeTamanho { get; set; } = string.Empty;

        [Column("nr_caixa")]
        public int? NrCaixa { get; set; }

        [Column("nr_pares")]
        public int? NrPares { get; set; }

        [Column("fg_rfid")]
        public bool? IdFgRf { get; set; }

        [Column("fg_status")]
        public int? FgStatus { get; set; }

        [Column("dt_embalagem")]
        public DateTime? DtEmbalagem { get; set; }

        [Column("dt_sorter")]
        public DateTime? DtSorter { get; set; }

        [Column("dt_estufamento")]
        public DateTime? DtEstufamento { get; set; }

        [Column("dt_expedicao")]
        public DateTime? DtExpedicao { get; set; }

        [Column("qt_peso")]
        public decimal? QtPeso { get; set; }
    }
}
