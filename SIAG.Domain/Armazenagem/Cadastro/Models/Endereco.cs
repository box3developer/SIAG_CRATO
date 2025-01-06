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

        [ForeignKey("RegiaoTrabalho")]
        [Column("id_regiao_trabalho")]
        public int IdRegiaoTrabalho { get; set; }
        public RegiaoTrabalho? RegiaoTrabalho { get; set; }

        [ForeignKey("SetorTrabalho")]
        [Column("id_setor_trabalho")]
        public int IdSetorTrabalho { get; set; }
        public SetorTrabalho? SetorTrabalho { get; set; }

        [ForeignKey("TipoEndereco")]
        [Column("id_tipo_endereco")]
        public int IdTipoEndereco { get; set; }
        public TipoEndereco? TipoEndereco { get; set; }

        [Column("nm_endereco")]
        public string NmEndereco { get; set; }

        [Column("qt_estoque_minimo")]
        public int QtEstoqueMinimo { get; set; }

        [Column("qt_estoque_maximo")]
        public int QtEstoqueMaximo { get; set; }

        [Column("fg_status")]
        public int FgStatus { get; set; }

        [Column("tp_preenchimento")]
        public int TpPreenchimento { get; set; }
    }
}
