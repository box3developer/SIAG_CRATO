using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SIAG.Domain.Armazenagem.Cadastro.Models
{
    public class Endereco
    {
        public int EnderecoId { get; set; }

        public int RegiaoTrabalhoId { get; set; }
        public RegiaoTrabalho RegiaoTrabalho { get; set; }

        public int SetorTrabalhoId { get; set; }
        public SetorTrabalho SetorTrabalho { get; set; }

        public int TipoEnderecoId { get; set; }
        public TipoEndereco TipoEndereco { get; set; }

        public string NmEndereco { get; set; }

        public int QtEstoqueMinimo { get; set; }

        public int QtEstoqueMaximo { get; set; }

        public int FgStatus { get; set; }

        public int TpPreenchimento { get; set; }
    }
}
