namespace SIAG_CRATO.Models;

public class ChamadaTarefaModel
{
    public int IdTarefa { get; set; }
    public Guid IdChamada { get; set; }
    public DateTime? DtInicio { get; set; }
    public DateTime? DtFim { get; set; }
}
