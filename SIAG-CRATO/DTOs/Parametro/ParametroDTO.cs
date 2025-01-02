namespace SIAG_CRATO.DTOs.Parametro;

public class ParametroDTO
{
    public int Id { get; set; }
    public string? Parametro { get; set; }
    public string? Valor { get; set; }
    public string? TipoParametro { get; set; }
    public string? Unidade { get; set; }
    public string? Tipo { get; set; }
    public bool? Visivel { get; set; }
    public int? Ativo { get; set; }
}
