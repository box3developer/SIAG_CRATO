using SIAG_CRATO.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG_CRATO.Models
{
    public class EnderecoModel
    {
        [Column("id_endereco")]
        public int IdEndereco { get; set; }

        [Column("id_regiaotrabalho")]
        public int Regiao { get; set; }

        [Column("id_setortrabalho")]
        public int Setor { get; set; }

        [Column("id_tipoendereco")]
        public int TipoEndereco { get; set; }

        [Column("nm_endereco")]
        public string Descricao { get; set; } = string.Empty;

        [Column("qt_estoqueminimo")]
        public int EstoqueMinimo { get; set; }

        [Column("qt_estoquemaximo")]
        public int EstoqueMaximo { get; set; }

        [Column("fg_status")]
        public StatusEndereco Status { get; set; }

        [Column("tp_preenchimento")]
        public TipoPreenchimento TipoPreenchimento { get; set; }
    }

}
