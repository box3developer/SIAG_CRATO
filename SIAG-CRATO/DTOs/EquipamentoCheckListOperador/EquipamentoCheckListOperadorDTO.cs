namespace SIAG_CRATO.DTOs.EquipamentoCheckListOperador;

public class EquipamentoCheckListOperadorDTO
{
    public int IdEquipamento { get; set; }
    public long IdOperador { get; set; }
    public int IdEquipamentoChecklist { get; set; }
    public bool FgResposta { get; set; }
    public DateTime? DtChecklist { get; set; }
}
