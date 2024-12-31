using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SIAG.Domain.Armazenagem.Cadastro.Models
{
    [Table("endereco")]
    public class Endereco
    {
        [Key]
        [Column("id_endereco")]
        public int EnderecoId { get; set; }

        [ForeignKey("RegiaoTrabalho")]
        [Column("id_regiaotrabalho")]
        public int RegiaoTrabalhoId { get; set; }
        public RegiaoTrabalho RegiaoTrabalho { get; set; }

        [ForeignKey("SetorTrabalho")]
        [Column("id_setortrabalho")]
        public int SetorTrabalhoId { get; set; }
        public SetorTrabalho SetorTrabalho { get; set; }

        [ForeignKey("TipoEndereco")]
        [Column("id_tipoendereco")]
        public int TipoEnderecoId { get; set; }
        public TipoEndereco TipoEndereco { get; set; }

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
