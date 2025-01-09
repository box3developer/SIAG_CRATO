using SIAG_CRATO.Data;

namespace SIAG_CRATO.Models;
public class OperadorModel
{
    public long IdOperador { get; set; }
    public string NmNfcOperador { get; set; } = string.Empty;
    public string NmCpf { get; set; } = string.Empty;
    public string NmOperador { get; set; } = string.Empty;
    public DateTime? DtLogin { get; set; }
    public Estabelecimentos NrLocalidade { get; set; }
    public FuncaoOperador FgFuncao { get; set; }
    public int IdResponsavel { get; set; }
    public OperadorModel? Responsavel { get; set; }
}
