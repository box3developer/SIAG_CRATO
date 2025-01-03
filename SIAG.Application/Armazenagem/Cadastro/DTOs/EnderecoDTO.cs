using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SIAG.Domain.Armazenagem.Cadastro.Models
{
    public class EnderecoDTO
    {
        public int EnderecoId { get; set; }

        public int RegiaoTrabalhoId { get; set; }
        public RegiaoTrabalhoDTO RegiaoTrabalho { get; set; }

        public int SetorTrabalhoId { get; set; }
        public SetorTrabalhoDTO SetorTrabalho { get; set; }

        public int TipoEnderecoId { get; set; }
        public TipoEnderecoDTO TipoEndereco { get; set; }

        public string NmEndereco { get; set; }

        public int QtEstoqueMinimo { get; set; }

        public int QtEstoqueMaximo { get; set; }

        public int FgStatus { get; set; }

        public int TpPreenchimento { get; set; }
    }
}
