namespace SIAG_CRATO.DTOs.LiderVirtual;

public class LiderVirtualDTO
{
    public string? LiderVirtualId { get; set; }
    public string? OperadorId { get; set; }
    public string? EquipamentoOrigemId { get; set; }
    public string? EquipamentoDestinoId { get; set; }
    public DateTime? DataLogin { get; set; }
    public DateTime? DataLogoff { get; set; }
    public string? OperadorLoginId { get; set; }
    public DateTime? DataLoginLimite { get; set; }
}
