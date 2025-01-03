using SIAG_CRATO.Data;

namespace SIAG_CRATO.Models;

public class AreaArmazenagemModel
{
    public long IdAreaArmazenagem { get; set; }
    public int IdTipoArea { get; set; }
    public int IdEndereco { get; set; }
    public Guid IdAgrupador { get; set; }
    public string? IdCaracol { get; set; }
    public int NrPosicaoX { get; set; }
    public int NrPosicaoY { get; set; }
    public int NrLado { get; set; }
    public StatusAreaArmazenagem FgStatus { get; set; }
    public string CdIdentificacao { get; set; } = string.Empty;
}
