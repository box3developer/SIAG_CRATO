namespace SIAG_CRATO.DTOs.ChamadaTarefa;

public class ChamdaTarefaDTO
{
    public int IdTarefa { get; set; }
    public Guid IdChamada { get; set; }
    public DateTime? DtInicio { get; set; }
    public DateTime? DtFim { get; set; }
}
