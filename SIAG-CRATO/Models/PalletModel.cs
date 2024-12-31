using SIAG_CRATO.Data;

namespace SIAG_CRATO.Models;
public class PalletModel
{
    public int Id_pallet { get; set; }
    public string Cd_identificacao { get; set; } = string.Empty;
    public int Qt_utilizacao { get; set; }
    public long Id_areaarmazenagem { get; set; }
    public Guid Id_agrupador { get; set; }
    public StatusPallet Fg_status { get; set; }
    public DateTime? Dt_ultimamovimentacao { get; set; }
}
