using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SIAG.Domain.Armazenagem.Cadastro.Models
{
    [Table("caixa")]
    public class Caixa
    {
        [Key]
        [Column("id_caixa")]
        public string CaixaId { get; set; }

        [ForeignKey("agrupadorativo")]
        [Column("id_agrupador")]
        public Guid AgrupadorId { get; set; }
        public AgrupadorAtivo Agrupador { get; set; }

        [Column("id_pallet")]
        public int PalletId { get; set; }

        [Column("id_programa")]
        public int ProgramaId { get; set; }

        [Column("id_pedido")]
        public int PedidoId { get; set; }

        [Column("cd_produto")]
        public string CdProduto { get; set; }

        [Column("cd_cor")]
        public string CdCor { get; set; }

        [Column("cd_gradetamanho")]
        public string CdGrudeTamanho { get; set; }

        [Column("nr_caixa")]
        public int NrCaixa { get; set; }

        [Column("nr_pares")]
        public int NrPares { get; set; }

        [Column("fg_rfid")]
        public bool FgRfid { get; set; }

        [Column("fg_status")]
        public int FgStatus { get; set; }

        [Column("dt_embalagem")]
        public DateTime DtEmbalagem { get; set; }

        [Column("dt_sorter")]
        public DateTime DtSorter { get; set; }

        [Column("dt_estufamento")]
        public DateTime DtEstufamento { get; set; }

        [Column("dt_expedicao")]
        public DateTime DtExpedicao { get; set; }

        [Column("qt_peso")]
        public decimal QtPeso { get; set; }
    }
}
