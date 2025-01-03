namespace SIAG_CRATO.Models;

public class CaixaLeituraModel
{
    public int? IdCaixaLeitura { get; set; }
    public string? IdCaixa { get; set; }
    public DateTime DtLeitura { get; set; }
    public int? FgTipo { get; set; }
    public int? FgStatus { get; set; }
    public int? IdOperador { get; set; }
    public int? IdEquipamento { get; set; }
    public int? IdPallet { get; set; }
    public long? IdAreaArmazenagem { get; set; }
    public int? IdEndereco { get; set; }
    public int? FgCancelado { get; set; }
    public int? IdOrdem { get; set; }
}