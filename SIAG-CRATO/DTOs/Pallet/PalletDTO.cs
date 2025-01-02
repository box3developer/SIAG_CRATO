using SIAG_CRATO.Data;

namespace SIAG_CRATO.DTOs.Pallet;

public class PalletDTO
{
    public int PalletId { get; set; }
    public string CodigoIdentificacao { get; set; } = string.Empty;
    public int QuantidadeUtilizacao { get; set; }
    public long AreaArmazenagemId { get; set; }
    public Guid AgrupadorId { get; set; }
    public StatusPallet Status { get; set; }
    public DateTime? DataUltimaMovimentacao { get; set; }
}
