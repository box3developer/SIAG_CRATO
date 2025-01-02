namespace SIAG_CRATO.DTOs.EquipamentoManutencao;

public class EquipamentoManutencaoDTO
{
    public int Id { get; set; }
    public int EquipamentoId { get; set; }
    public int TipoManutencao { get; set; }
    public int DataInicio { get; set; }
    public int DataFim { get; set; }
}
