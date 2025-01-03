using System.Text.Json.Serialization;
using SIAG_CRATO.Data;

namespace SIAG_CRATO.DTOs.Chamada;

public class ChamadaInsertDTO
{
    [JsonIgnore]
    public Guid IdChamada { get; set; }

    public int IdPalletOrigem { get; set; }
    public int IdPalletDestino { get; set; }
    public long IdAreaArmazenagemOrigem { get; set; }
    public long IdAreaArmazenagemDestino { get; set; }
    public int IdAtividade { get; set; }

    [JsonIgnore]
    public StatusChamada FgStatus { get; set; }
    public bool FgPriorizar { get; set; }
}
