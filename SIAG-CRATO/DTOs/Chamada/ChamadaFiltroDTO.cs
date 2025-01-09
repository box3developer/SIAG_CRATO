using SIAG_CRATO.Data;

namespace SIAG_CRATO.DTOs.Chamada;

public class ChamadaFiltroDTO
{
    public ChamadaDTO? Chamada { get; set; }
    public List<StatusChamada> ListaStatusChamada { get; set; } = [];
}
