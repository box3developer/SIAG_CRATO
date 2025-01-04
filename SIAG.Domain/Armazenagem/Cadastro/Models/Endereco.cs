using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG.Domain.Armazenagem.Cadastro.Models
{
    public class Endereco
    {
        [Key]
        public int EnderecoId { get; set; }

        [ForeignKey("RegiaoTrabalhoModel")]
        public int RegiaoTrabalhoId { get; set; }
        public RegiaoTrabalho RegiaoTrabalho { get; set; }

        [ForeignKey("SetorTrabalhoModel")]
        public int SetorTrabalhoId { get; set; }
        public SetorTrabalho SetorTrabalho { get; set; }

        [ForeignKey("TipoEnderecoModel")]
        public int TipoEnderecoId { get; set; }
        public TipoEndereco TipoEndereco { get; set; }

        public string NmEndereco { get; set; }

        public int QtEstoqueMinimo { get; set; }

        public int QtEstoqueMaximo { get; set; }

        public int FgStatus { get; set; }

        public int TpPreenchimento { get; set; }
    }
}
