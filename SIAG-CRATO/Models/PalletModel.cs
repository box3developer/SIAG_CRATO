using SIAG_CRATO.Data;

namespace SIAG_CRATO.Models;
public class PalletModel
{
    public int IdPallet { get; set; }
    public string CdIdentificacao { get; set; } = string.Empty;
    public int QtUtilizacao { get; set; }
    public long? IdAreaArmazenagem { get; set; }
    public Guid IdAgrupador { get; set; }
    public StatusPallet FgStatus { get; set; }
    public DateTime? DtUltimaMovimentacao { get; set; }
}
