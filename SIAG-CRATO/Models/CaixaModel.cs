namespace SIAG_CRATO.Models;

public class CaixaModel
{
    public string Id_caixa { get; set; } = string.Empty;
    public Guid? Id_agrupador { get; set; }
    public string? Id_pallet { get; set; }
    public int? Id_programa { get; set; }
    public string? Id_pedido { get; set; }
    public string? Cd_produto { get; set; }
    public string? Cd_cor { get; set; }
    public string? Cd_gradetamanho { get; set; }
    public int? Nr_caixa { get; set; }
    public int? Nr_pares { get; set; }
    public bool? Fg_rfid { get; set; }
    public int? Fg_status { get; set; }
    public DateTime? Dt_embalagem { get; set; }
    public DateTime? Dt_expedicao { get; set; }
    public DateTime? Dt_sorter { get; set; }
    public DateTime? Dt_estufamento { get; set; }
    public DateTime Dt_leitura { get; set; }
}
