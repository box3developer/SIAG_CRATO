namespace SIAG_CRATO.Models;

public class AtividadeTarefaModel
{
    public int IdTarefa { get; set; }
    public string NmTarefa { get; set; } = string.Empty;
    public string NmMensagem { get; set; } = string.Empty;
    public int IdAtividade { get; set; }
    public int CdSequencia { get; set; }
    public int? FgRecurso { get; set; }
    public int IdAtividadeRotina { get; set; }
    public int QtPotenciaNormal { get; set; }
    public int QtPotenciaAumentada { get; set; }
}
