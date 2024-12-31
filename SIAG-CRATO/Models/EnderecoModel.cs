using SIAG_CRATO.Data;

namespace SIAG_CRATO.Models;

public class EnderecoModel
{
    public int Id_endereco { get; set; }
    public int Id_regiaotrabalho { get; set; }
    public int Id_setortrabalho { get; set; }
    public int Id_tipoendereco { get; set; }
    public string Nm_endereco { get; set; } = string.Empty;
    public int Qt_estoqueminimo { get; set; }
    public int Qt_estoquemaximo { get; set; }
    public StatusEndereco Fg_status { get; set; }
    public TipoPreenchimento Tp_preenchimento { get; set; }
}
