namespace SIAG_CRATO.DTOs.ChamadaTarefa;

public class ChamdaTarefaDTO
{
    public int TarefaId { get; set; }
    public Guid ChamadaId { get; set; }
    public DateTime? DataInicio { get; set; }
    public DateTime? DataFim { get; set; }
}
