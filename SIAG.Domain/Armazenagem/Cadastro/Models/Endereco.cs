using SIAG.Domain.Armazenagem.Cadastro.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG.Domain.Armazenagem.Cadastro.Models
{
    [BasicEntity]
    [Table("endereco")]
    public class Endereco
    {
        [Key]
        [Column("id_endereco")]
        public int IdEndereco { get; set; }

        [Column("id_regiaotrabalho")]
        public int IdRegiaoTrabalho { get; set; }

        [ForeignKey(nameof(IdRegiaoTrabalho))]
        public RegiaoTrabalho? RegiaoTrabalho { get; set; }


        [Column("id_setortrabalho")]
        public int IdSetorTrabalho { get; set; }

        [ForeignKey(nameof(IdSetorTrabalho))]
        public SetorTrabalho? SetorTrabalho { get; set; }


        [Column("id_tipoendereco")]
        public int IdTipoEndereco { get; set; }

        [ForeignKey(nameof(IdTipoEndereco))]
        public TipoEndereco? TipoEndereco { get; set; }



        [Column("nm_endereco")]
        public string NmEndereco { get; set; } = string.Empty;

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
