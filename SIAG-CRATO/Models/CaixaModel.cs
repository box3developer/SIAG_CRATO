using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG_CRATO.Models
{
    public class CaixaModel
    {
        [Column("id_caixa")]
        public string IdCaixa { get; set; } = string.Empty;

        [Column("id_agrupador")]
        public Guid? IdAgrupador { get; set; }

        [Column("id_pallet")]
        public int? IdPallet { get; set; }

        [Column("id_programa")]
        public int? IdPrograma { get; set; }

        [Column("id_pedido")]
        public int? IdPedido { get; set; }

        [Column("cd_produto")]
        public string? CdProduto { get; set; }

        [Column("cd_cor")]
        public string? CdCor { get; set; }

        [Column("cd_gradetamanho")]
        public string? CdGradeTamanho { get; set; }

        [Column("nr_caixa")]
        public int? NrCaixa { get; set; }

        [Column("nr_pares")]
        public int? NrPares { get; set; }

        [Column("fg_rfid")]
        public bool? Rfid { get; set; }

        [Column("fg_status")]
        public int? Status { get; set; }

        [Column("dt_embalagem")]
        public DateTime? DtEmbalagem { get; set; }

        [Column("dt_sorter")]
        public DateTime? DtSorter { get; set; }

        [Column("dt_estufamento")]
        public DateTime? DtEstufamento { get; set; }

        [Column("dt_expedicao")]
        public DateTime? DtExpedicao { get; set; }

        [Column("qt_peso")]
        public decimal? Peso { get; set; }
    }
}
