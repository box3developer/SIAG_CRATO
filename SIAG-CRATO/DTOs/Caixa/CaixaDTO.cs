namespace SIAG_CRATO.DTOs.Caixa;

public class CaixaDTO
{
    public string CaixaId { get; set; } = string.Empty;
    public Guid? AgrupadorId { get; set; }
    public string? PalletId { get; set; }
    public int? ProgramaId { get; set; }
    public string? PedidoId { get; set; }
    public string? CodigoProduto { get; set; }
    public string? CodigoCor { get; set; }
    public string? CodigoGradeTamanho { get; set; }
    public int? NumeroCaixa { get; set; }
    public int? Pares { get; set; }
    public bool? RFID { get; set; }
    public int? Status { get; set; }
    public DateTime? DataEmbalagem { get; set; }
    public DateTime? DataExpedicao { get; set; }
    public DateTime? DataSorter { get; set; }
    public DateTime? DataEstufamento { get; set; }
    public DateTime DataLeitura { get; set; }
}
