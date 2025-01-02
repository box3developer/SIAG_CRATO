using SIAG_CRATO.Data;

namespace SIAG_CRATO.DTOs.Operador;

public class OperadorDTO
{
    public long Codigo { get; set; }
    public string NFC { get; set; } = string.Empty;
    public string CPF { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public DateTime? DataLogin { get; set; }
    public Estabelecimentos Localidade { get; set; }
    public FuncaoOperador FuncaoOperador { get; set; }
    public int ResponsavelId { get; set; }
}
