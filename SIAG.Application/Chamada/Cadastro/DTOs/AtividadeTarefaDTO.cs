namespace SIAG.Application.Chamada.Cadastro.DTOs;

public class AtividadeTarefaDTO
{
    public int IdTarefa { get; set; }
    public string NmTarefa { get; set; } = string.Empty;
    public string NmMensagem { get; set; } = string.Empty;
    public int IdAtividade { get; set; }
    public int CdSequencia { get; set; }
    public int? FgRecurso { get; set; }
    public int IdAtividadeRotina { get; set; }
    public AtividadeRotinaDTO? AtividadeRotina { get; set; }
    public int QtPotenciaNormal { get; set; }
    public int QtPotenciaAumentada { get; set; }
}
