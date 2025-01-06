using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG.Domain.Armazenagem.Cadastro.Models
{
    [Table("caixaleitura")]
    public class Caixaleitura
    {
        [Key]
        [Column("id_caixaleitura")]
        public long IdCaixaleitura { get; set; }

        [Column("dt_leitura")]
        public DateTime DtLeitura { get; set; }

        [Column("fg_cancelado")]
        public bool FgCancelado { get; set; }

        [Column("fg_status")]
        public int FgStatus { get; set; }

        [Column("fg_tipo")]
        public int FgTipo { get; set; }

        [Column("id_areaarmazenagem")]
        public long IdAreaarmazenagem { get; set; }

        [Column("id_caixa")]
        public string IdCaixa { get; set; }

        [Column("id_endereco")]
        public int IdEndereco { get; set; }

        [Column("id_equipamento")]
        public int IdEquipamento { get; set; }

        [Column("id_operador")]
        public long IdOperador { get; set; }

        [Column("id_ordem")]
        public long IdOrdem { get; set; }

        [Column("id_pallet")]
        public int IdPallet { get; set; }

    }

}
