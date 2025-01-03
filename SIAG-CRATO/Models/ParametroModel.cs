namespace SIAG_CRATO.Models;

public class ParametroModel
{
    public int IdParametro { get; set; }
    public string? NmParametro { get; set; }
    public string? NmValor { get; set; }
    public string? FgTipoParametro { get; set; }
    public string? NmUnidadeMedida { get; set; }
    public string? NmTipo { get; set; }
    public bool? FgVisivel { get; set; }
    public int? FgAtivo { get; set; }
}
