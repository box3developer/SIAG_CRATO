using SIAG_CRATO.Data;
using SIAG_CRATO.Models;

namespace SIAG_CRATO.DTOs.Operador;

public class OperadorDTO
{
    public long IdOperador { get; set; }
    public string NFC { get; set; } = string.Empty;
    public string NmCpf { get; set; } = string.Empty;
    public string NmOperador { get; set; } = string.Empty;
    public DateTime? DtLogin { get; set; }
    public Estabelecimentos NrLocalidade { get; set; }
    public FuncaoOperador FgFuncao { get; set; }
    public int IdResponsavel { get; set; }
    public OperadorModel? Responsavel { get; set; }
}
