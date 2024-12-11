using SIAG_CRATO.Data;

namespace SIAG_CRATO.Models;
public class EquipamentoModeloModel
{
    public int Codigo { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public StatusModeloEquipamento Status { get; set; }
}
