namespace SIAG_CRATO.Models;

public class AtividadePrioridadeModel
{
    public int IdAtividadePrioridade { get; set; }
    public int FgTipo { get; set; }
    public string NmProcedure { get; set; } = string.Empty;
    public int QtPontuacao { get; set; }
}
