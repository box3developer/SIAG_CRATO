namespace SIAG_CRATO.DTOs.EquipamentoCheckListOperador;

public class EquipamentoCheckListOperadorDTO
{
    public int EquipamentoId { get; set; }
    public long OperadorId { get; set; }
    public int ChecklistId { get; set; }
    public bool Resposta { get; set; }
    public DateTime? Data { get; set; }
}
