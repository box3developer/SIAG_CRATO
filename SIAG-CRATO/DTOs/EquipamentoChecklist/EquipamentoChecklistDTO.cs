namespace SIAG_CRATO.DTOs.EquipamentoCheckList;

public class EquipamentoChecklistDTO
{
    public int EquipamentoChecklistId { get; set; }
    public int EquipamentoModeloId { get; set; }
    public string? NomeDescricao { get; set; }
    public bool? Critico { get; set; }
    public int? Status { get; set; }
}
