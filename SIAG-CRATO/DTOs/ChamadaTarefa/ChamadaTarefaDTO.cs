namespace SIAG_CRATO.DTOs.ChamadaTarefa;

public class ChamadaTarefaDTO
{
    public int IdTarefa { get; set; }
    public Guid IdChamada { get; set; }
    public DateTime? DtInicio { get; set; }
    public DateTime? DtFim { get; set; }
}
