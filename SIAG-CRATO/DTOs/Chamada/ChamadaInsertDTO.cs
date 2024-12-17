using System.Text.Json.Serialization;
using SIAG_CRATO.Data;

namespace SIAG_CRATO.DTOs.Chamada;

public class ChamadaInsertDTO
{
    [JsonIgnore]
    public Guid Codigo { get; set; }

    public int PalletOrigemId { get; set; }
    public int PalletDestinoId { get; set; }
    public long AreaArmazenagemOrigemId { get; set; }
    public long AreaArmazenagemDestinoId { get; set; }
    public int AtividadeId { get; set; }

    [JsonIgnore]
    public StatusChamada Status { get; set; }
    public bool Priorizar { get; set; }
}
