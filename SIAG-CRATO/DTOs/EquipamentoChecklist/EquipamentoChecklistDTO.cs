namespace SIAG_CRATO.DTOs.EquipamentoCheckList;

public class EquipamentoChecklistDTO
{
    public int IdEquipamentoChecklist { get; set; }
    public int IdEquipamentoModelo { get; set; }
    public string? NmDescricao { get; set; }
    public bool? FgCritico { get; set; }
    public int? FgStatus { get; set; }
}
