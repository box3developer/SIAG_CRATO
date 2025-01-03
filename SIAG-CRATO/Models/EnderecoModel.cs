using SIAG_CRATO.Data;

namespace SIAG_CRATO.Models;

public class EnderecoModel
{
    public int IdEndereco { get; set; }
    public int IdRegiaoTrabalho { get; set; }
    public int IdSetorTrabalho { get; set; }
    public int IdTipoEndereco { get; set; }
    public string NmEndereco { get; set; } = string.Empty;
    public int QtEstoqueMinimo { get; set; }
    public int QtEstoqueMaximo { get; set; }
    public StatusEndereco FgStatus { get; set; }
    public TipoPreenchimento TpPreenchimento { get; set; }
}
