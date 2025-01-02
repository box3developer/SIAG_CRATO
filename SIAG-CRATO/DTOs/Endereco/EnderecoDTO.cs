using SIAG_CRATO.Data;

namespace SIAG_CRATO.DTOs.Endereco;

public class EnderecoDTO
{
    public int EnderecoId { get; set; }
    public int RegiaoTrabalhoId { get; set; }
    public int SetorId { get; set; }
    public int TipoEnderecoId { get; set; }
    public string NomeEndereco { get; set; } = string.Empty;
    public int EstoqueMinimo { get; set; }
    public int EstoqueMaximo { get; set; }
    public StatusEndereco Status { get; set; }
    public TipoPreenchimento TipoPreenchimento { get; set; }
}
