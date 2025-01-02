using SIAG_CRATO.Data;

namespace SIAG_CRATO.DTOs.AtividadeRotina;

public class AtividadeRotinaDTO
{
    public int Codigo { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Procedure { get; set; } = string.Empty;
    public TipoRotina Tipo { get; set; }
}
