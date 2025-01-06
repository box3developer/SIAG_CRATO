using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG.Domain.Armazenagem.Cadastro.Models
{
    [Table("caixa")]
    public class Caixa
    {
        [Key]
        [Column("id_caixa")]
        public string IdCaixa { get; set; }

        [ForeignKey("agrupadorativo")]
        [Column("agrupador_id")]
        public Guid AgrupadorId { get; set; }
        public AgrupadorAtivo? Agrupador { get; set; }

        [Column("id_pallet")]
        public int IdPallet { get; set; }

        [Column("id_programa")]
        public int IdPrograma { get; set; }

        [Column("id_pedido")]
        public int IdPedido { get; set; }

        [Column("cd_produto")]
        public string CdProduto { get; set; } = string.Empty;

        [Column("cd_cor")]
        public string CdCor { get; set; } = string.Empty;

        [Column("cd_grude_tamanho")]
        public string CdGrudeTamanho { get; set; } = string.Empty;

        [Column("nr_caixa")]
        public int NrCaixa { get; set; }

        [Column("nr_pares")]
        public int NrPares { get; set; }

        [Column("id_fg_rf")]
        public bool IdFgRf { get; set; }

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
