using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG.Domain.Armazenagem.Cadastro.Models
{
    [Table("endereco")]
    public class Endereco
    {
        [Key]
        [Column("id_endereco")]
        public int IdEndereco { get; set; }

        [ForeignKey("regiaotrabalho")]
        [Column("id_regiaotrabalho")]
        public int IdRegiaoTrabalho { get; set; }
        public RegiaoTrabalho? RegiaoTrabalho { get; set; }

        [ForeignKey("setortrabalho")]
        [Column("id_setortrabalho")]
        public int IdSetorTrabalho { get; set; }
        public SetorTrabalho? SetorTrabalho { get; set; }

        [ForeignKey("tipoendereco")]
        [Column("id_tipoendereco")]
        public int IdTipoEndereco { get; set; }
        public TipoEndereco? TipoEndereco { get; set; }

        [Column("nm_endereco")]
        public string NmEndereco { get; set; }

        [Column("qt_estoqueminimo")]
        public int QtEstoqueMinimo { get; set; }

        [Column("qt_estoquemaximo")]
        public int QtEstoqueMaximo { get; set; }

        [Column("fg_status")]
        public int FgStatus { get; set; }

        [Column("tp_preenchimento")]
        public int TpPreenchimento { get; set; }
    }
}
