namespace SIAG_CRATO.Models;

public class EquipamentoManutencaoModel
{
    public int IdEquipamentoManutencao { get; set; }
    public int IdEquipamento { get; set; }
    public int FgTipoManutencao { get; set; }
    public int DtInicio { get; set; }
    public int DtFim { get; set; }
}
