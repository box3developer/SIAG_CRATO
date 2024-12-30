namespace SIAG_CRATO.DTOs.Caixa;

public class CaixaPedidoDTO
{
    public string Codigo { get; set; } = string.Empty;
    public string Produto { get; set; } = string.Empty;
    public string Cor { get; set; } = string.Empty;
    public string GradeTamanho { get; set; } = string.Empty;
    public int Pares { get; set; }
}
