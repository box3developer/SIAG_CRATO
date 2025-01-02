namespace SIAG_CRATO.DTOs.CaixaLeitura;

public class CaixaLeituraDTO
{
    public string? CaixaLeituraId { get; set; }
    public string? CaixaId { get; set; }
    public DateTime DataLeitura { get; set; }
    public int? Tipo { get; set; }
    public int? Status { get; set; }
    public string? OperadorId { get; set; }
    public string? EquipamentoId { get; set; }
    public string? PalletId { get; set; }
    public string? AreaArmazenagemId { get; set; }
    public string? EnderecoId { get; set; }
    public int? Cancelado { get; set; }
    public string? Ordem { get; set; }
}
