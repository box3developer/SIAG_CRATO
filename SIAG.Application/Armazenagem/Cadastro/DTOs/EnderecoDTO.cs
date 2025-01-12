namespace SIAG.Application.Armazenagem.Cadastro.DTOs
{
    public class EnderecoDTO
    {
        public int IdEndereco { get; set; }

        public int IdRegiaoTrabalho { get; set; }
        public RegiaoTrabalhoDTO? RegiaoTrabalho { get; set; }


        public int IdSetorTrabalho { get; set; }
        public SetorTrabalhoDTO? SetorTrabalho { get; set; }


        public int IdTipoEndereco { get; set; }
        public TipoEnderecoDTO? TipoEndereco { get; set; }


        public string NmEndereco { get; set; } = string.Empty;
        public int QtEstoqueMinimo { get; set; }
        public int QtEstoqueMaximo { get; set; }
        public int FgStatus { get; set; }
        public int TpPreenchimento { get; set; }
    }
}
